using System.Collections.Generic;
using UnityEngine;

public class PuzzleAutopilot : SelectableItem, IPuzzle {

    public PuzzleAutopilotData Data;

    public AutopilotIO AutopilotInteractable;

    int[] currentCombinantion = new int[2];

    #region IPuzzle
    PuzzleState _solutionState = PuzzleState.Unsolved;
    public PuzzleState SolutionState {
        get {
            return _solutionState;
        }

        set {
            _solutionState = value;
        }
    }

    public void Setup(IPuzzleData _data) {
        Data = _data as PuzzleAutopilotData;
    }

    public void OnButtonSelect(SelectableButton _button) { }
    public void OnSwitchSelect(SelectableSwitch _switch) { }
    public void OnMonitorSelect(SelectableMonitor _monitor) { }
    public void OnUpdateSelectable(SelectableAbstract _selectable) { }
    #endregion

    public void SetInput(InputValue _inputSent) {

        //PuzzleAutopilotButton button = _selectable as PuzzleAutopilotButton;

        //if (!isFase1Completed) {
        //    if(Data.Fase1[currentCombinantion[0]].Solution[currentSolutionIndex] == button.Actualvalue) {
        //        currentSolutionIndex++;
        //        if (currentSolutionIndex > Data.Fase1[currentCombinantion[0]].Solution.Count) {
        //            isFase1Completed = true;
        //            currentSolutionIndex = 0;
        //            MonitorStandard[1].ToggleOnOff(true);
        //            return;
        //        }
        //    }
        //} else if (!isFase2Completed) {
        //    if (Data.Fase2[currentCombinantion[0]].Solution[currentSolutionIndex] == button.Actualvalue) {
        //        currentSolutionIndex++;
        //        if (currentSolutionIndex > Data.Fase2[currentCombinantion[0]].Solution.Count) {
        //            isFase2Completed = true;
        //            currentSolutionIndex = 0;
        //            MonitorStandard[2].ToggleOnOff(true);
        //            DoWinningthings();
        //            return;
        //        }
        //    }
        //} 

        //DoBadthings();
    }

    void DoWinningthings() { SolutionState = PuzzleState.Solved; }
    void DoBadthings() { SolutionState = PuzzleState.Broken; }

    protected override void OnInitEnd(SelectableAbstract _parent) {
        //Crea un setupIniziale;
        GenerateInitialValues();
        InitSwitches();
        InitButtons();
        InitOutputMonitors();
    }

    void InitSwitches()
    {
        AutopilotInteractable.LevaDx.specificBehaviour = new PuzzleAutoPilotSwitch((InputValue)Random.Range(-2, 0));
        AutopilotInteractable.LevaDx.Init(this);
        AutopilotInteractable.LevaSx.specificBehaviour = new PuzzleAutoPilotSwitch((InputValue)Random.Range(-4, -2));
        AutopilotInteractable.LevaDx.Init(this);
    }

    void InitButtons() {

        for (int i = 0; i < 6; i++)
        {
            switch (i)
            {
                case 0:
                    AutopilotInteractable.ButtonA.DataInjection(new PuzzleAutopilotButtonData() { Actualvalue = InputValue.BottoneA });
                    AutopilotInteractable.ButtonA.Init(this);
                    break;
                case 1:
                    AutopilotInteractable.ButtonB.DataInjection(new PuzzleAutopilotButtonData() { Actualvalue = InputValue.BottoneB });
                    AutopilotInteractable.ButtonB.Init(this);
                    break;
                case 2:
                    AutopilotInteractable.ButtonF.DataInjection(new PuzzleAutopilotButtonData() { Actualvalue = InputValue.BottoneF });
                    AutopilotInteractable.ButtonF.Init(this);
                    break;
                case 3:
                    AutopilotInteractable.ButtonG.DataInjection(new PuzzleAutopilotButtonData() { Actualvalue = InputValue.BottoneG });
                    AutopilotInteractable.ButtonG.Init(this);
                    break;
                case 4:
                    AutopilotInteractable.ButtonK.DataInjection(new PuzzleAutopilotButtonData() { Actualvalue = InputValue.BottoneK });
                    AutopilotInteractable.ButtonK.Init(this);
                    break;
                case 5:
                    AutopilotInteractable.ButtonL.DataInjection(new PuzzleAutopilotButtonData() { Actualvalue = InputValue.BottoneL });
                    AutopilotInteractable.ButtonL.Init(this);
                    break;
                default:
                    break;
            }
        }
    }

    void InitOutputMonitors()
    {
        OutputValue output = Data.Fase1[currentCombinantion[0]].MonitorOutput;
        AutopilotInteractable.MonitorFase1.SetMaterial((int)output);

        OutputValue output2 = Data.Fase2[currentCombinantion[1]].MonitorOutput;
        AutopilotInteractable.MonitorFase2.SetMaterial((int)output2);

        AutopilotInteractable.MonitorFase2.ToggleOnOff(false);
        AutopilotInteractable.MonitorFaseOK.ToggleOnOff(false);
    }

    //genera a caso una combinazione iniziale del puzzle (e la soluzione)
    void GenerateInitialValues() {
        int fase1index = Random.Range(0, Data.Fase1.Count);
        currentCombinantion[0] = fase1index;

        int fase2index = Random.Range(0, Data.Fase2.Count);
        currentCombinantion[1] = fase2index;
    }

    [System.Serializable]
    public struct AutopilotIO
    {
        public SelectableButton ButtonA;
        public SelectableButton ButtonB;
        public SelectableButton ButtonF;
        public SelectableButton ButtonG;
        public SelectableButton ButtonK;
        public SelectableButton ButtonL;

        public PuzzleAutopilotOutputMonitor MonitorFase1;
        public PuzzleAutopilotOutputMonitor MonitorFase2;
        public PuzzleAutopilotOutputMonitor MonitorFaseOK;

        public SelectableSwitch LevaSx;
        public SelectableSwitch LevaDx;
    }

    public enum InputValue{
        LevaSxSX = -4,
        LevaSxDX,
        LevaDxSx,
        LevaDxDx,
        BottoneA = 0,
        BottoneB,
        BottoneF,
        BottoneG,
        BottoneK,
        BottoneL
    }
    public enum OutputValue
    {
        C,D,E,H,I,J
    }
}

