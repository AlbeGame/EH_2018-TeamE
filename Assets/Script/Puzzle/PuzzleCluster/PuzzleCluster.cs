using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectableBehaviour), typeof(PuzzleGraphic))]
public class PuzzleCluster : MonoBehaviour, IPuzzle, ISelectable
{
    SelectableBehaviour selectable;
    PuzzleGraphic graphicCtrl;

    PuzzleClusterData data;
    List<PuzzleClusterData.Sequence> chosenSeq = new List<PuzzleClusterData.Sequence>();
    ClusterColor chosenColor;
    int currentSeq;
    List<ClusterButton> buttonToSolve = new List<ClusterButton>();

    private float _heatLvl;
    private float heatLvl
    {
        get { return _heatLvl; }
        set
        {
            if (value > 100)
                _heatLvl = 100;
            else if (value < 0)
                _heatLvl = 0;
            else
                _heatLvl = value;

            Interactables.HeatBar.SetFillAmount(_heatLvl, false);
            if (_heatLvl == 100)
                DoLoose();
        }
    }

    bool checkPressed;

    int misstakes;

    public Material LightRedMat;
    public Material LightYellowMat;
    public Material LightGreenMat;
    public Material LightBlueMat;
    public ClusterIO Interactables;

    public PuzzleState SolutionState { get; set; }

    public bool CheckIfSolved()
    {
        throw new System.NotImplementedException();
    }

    public void DoLoose()
    {
        SolutionState = PuzzleState.Broken;
        selectable.GetRoot().GetComponent<LevelManager>().NotifyPuzzleBreakdown(this);

        graphicCtrl.Paint(SolutionState);
    }

    public void DoWin()
    {
        SolutionState = PuzzleState.Solved;
        selectable.GetRoot().GetComponent<LevelManager>().NotifyPuzzleSolved(this);

        graphicCtrl.Paint(SolutionState);
    }

    float timeGone;
    private void Update()
    {
        if (checkPressed && SolutionState == PuzzleState.Unsolved)
        {
            timeGone += Time.deltaTime;
            if (timeGone > data.SequenceDuration)
            {
                SetNewSequence();
            }
        }

        UpdateHeatBar();
    }

    #region OnSelection Behaviours
    public void OnButtonSelect(SelectableButton _button)
    {

        ClusterButtonData btn = _button.InputData as ClusterButtonData;

        if (!checkPressed && btn.BtnType == ClusterButton.Check)
            StartSequences();
        else
        {
            if (buttonToSolve.Contains(btn.BtnType))
            {
                buttonToSolve.Remove(btn.BtnType);
                if (buttonToSolve.Count == 0)
                    DoWin();
            }
            else
            {
                misstakes++;
                SetNewSequence();
            }
        }

        if (SolutionState != PuzzleState.Solved)
            selectable.Select();
    }

    public void OnMonitorSelect(SelectableMonitor _monitor)
    {
        throw new System.NotImplementedException();
    }

    public void OnSelection()
    {
    }

    public void OnStateChange(SelectionState _state)
    {
    }

    public void OnSwitchSelect(SelectableSwitch _switch)
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdateSelectable(IPuzzleInput _input)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    public void Setup(IPuzzleData _data)
    {
        selectable = GetComponent<SelectableBehaviour>();
        graphicCtrl = GetComponent<PuzzleGraphic>();

        data = _data as PuzzleClusterData;
        if (data.Sequences.Count < data.SequencesAmount)
        {
            Debug.LogError("ATTEZIONE: nel setup del PuzzleCluster non ci sono abbastanza sequenze possibili" +
                "per sceglierne quante richiesto (SequenceAmount)!" +
                "Valore cambiato via codice per funzionare (I DATI SONO STATI CAMBIATI)!");

            data.SequencesAmount = data.Sequences.Count;
        }

        Interactables.CheckBtn.Init(this, new ClusterButtonData() { BtnType = ClusterButton.Check });
        Interactables.A1_Btn.Init(this, new ClusterButtonData() { BtnType = ClusterButton.A1 });
        Interactables.A2_Btn.Init(this, new ClusterButtonData() { BtnType = ClusterButton.A2 });
        Interactables.A3_Btn.Init(this, new ClusterButtonData() { BtnType = ClusterButton.A3 });
        Interactables.B4_Btn.Init(this, new ClusterButtonData() { BtnType = ClusterButton.B4 });
        Interactables.B5_Btn.Init(this, new ClusterButtonData() { BtnType = ClusterButton.B5 });
        Interactables.B6_Btn.Init(this, new ClusterButtonData() { BtnType = ClusterButton.B6 });
    }

    public void Init()
    {
        List<PuzzleClusterData.Sequence> newSeq = new List<PuzzleClusterData.Sequence>();
        newSeq = data.Sequences;
        while (chosenSeq.Count < data.SequencesAmount)
        {
            PuzzleClusterData.Sequence seq = newSeq[Random.Range(0, newSeq.Count)];
            chosenSeq.Add(seq);
            newSeq.Remove(seq);
        }
        chosenColor = (ClusterColor)Random.Range(0, 4);

        SolutionState = PuzzleState.Unsolved;

        checkPressed = false;
        misstakes = 0;
        heatLvl = 0;
        Interactables.CheckLight.materials[1] = data.LightOffMat;
        SetScreens(false);
    }

    void SetScreens(bool _on)
    {
        if (!_on)
        {
            Interactables.A1_Screen.material = data.ScreenOffMat;
            Interactables.A2_Screen.material = data.ScreenOffMat;
            Interactables.A3_Screen.material = data.ScreenOffMat;
            Interactables.B4_Screen.material = data.ScreenOffMat;
            Interactables.B5_Screen.material = data.ScreenOffMat;
            Interactables.B6_Screen.material = data.ScreenOffMat;
        }
        else
        {
            Interactables.A1_Screen.material = chosenSeq[currentSeq].A1_Mat;
            Interactables.A2_Screen.material = chosenSeq[currentSeq].A2_Mat;
            Interactables.A3_Screen.material = chosenSeq[currentSeq].A3_Mat;
            Interactables.B4_Screen.material = chosenSeq[currentSeq].B4_Mat;
            Interactables.B5_Screen.material = chosenSeq[currentSeq].B5_Mat;
            Interactables.B6_Screen.material = chosenSeq[currentSeq].B6_Mat;
        }
    }

    void UpdateHeatBar()
    {
        if (checkPressed && SolutionState == PuzzleState.Unsolved)
            heatLvl += Time.deltaTime * (data.HeatBarBaseFilling + data.HeatBarErrorFilling * misstakes);
    }

    void SetNewSequence()
    {
        timeGone = 0;
        int oldSeq = currentSeq;
        while (oldSeq == currentSeq)
        {
            currentSeq = Random.Range(0, chosenSeq.Count);
        }

        switch (chosenColor)
        {
            case ClusterColor.Red:
                buttonToSolve = chosenSeq[currentSeq].RedSolution;
                break;
            case ClusterColor.Yellow:
                buttonToSolve = chosenSeq[currentSeq].YellowSolution;
                break;
            case ClusterColor.Green:
                buttonToSolve = chosenSeq[currentSeq].GreenSolution;
                break;
            case ClusterColor.Blue:
                buttonToSolve = chosenSeq[currentSeq].BlueSolution;
                break;
        }

        SetScreens(true);
    }

    void StartSequences()
    {
        checkPressed = true;

        switch (chosenColor)
        {
            case ClusterColor.Red:
                Interactables.CheckLight.materials[1] = LightRedMat;
                break;
            case ClusterColor.Yellow:
                Interactables.CheckLight.materials[1] = LightYellowMat;
                break;
            case ClusterColor.Green:
                Interactables.CheckLight.materials[1] = LightGreenMat;
                break;
            case ClusterColor.Blue:
                Interactables.CheckLight.materials[1] = LightBlueMat;
                break;
        }
        SetNewSequence();

        Debugger.DebugLogger.LogText(chosenColor.ToString());
        Debugger.DebugLogger.LogText(buttonToSolve.ToString());
    }

    public enum ClusterColor
    {
        Red, Yellow, Green, Blue
    }

    public enum ClusterButton
    {
        A1, A2, A3, B4, B5, B6, Check
    }

    [System.Serializable]
    public struct ClusterIO
    {
        [Header("Input")]
        public SelectableButton CheckBtn;
        public SelectableButton A1_Btn;
        public SelectableButton A2_Btn;
        public SelectableButton A3_Btn;
        public SelectableButton B4_Btn;
        public SelectableButton B5_Btn;
        public SelectableButton B6_Btn;

        [Header("Output")]
        public SliderController HeatBar;
        public MeshRenderer CheckLight;
        public MeshRenderer A1_Screen;
        public MeshRenderer A2_Screen;
        public MeshRenderer A3_Screen;
        public MeshRenderer B4_Screen;
        public MeshRenderer B5_Screen;
        public MeshRenderer B6_Screen;
    }

    public class ClusterButtonData : IPuzzleInputData
    {
        public ClusterButton BtnType;
    }
}
