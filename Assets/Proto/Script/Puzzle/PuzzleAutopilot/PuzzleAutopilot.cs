using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleAutopilot : SelectableItem, IPuzzle {

    public PuzzleAutopilotData Data;

    public AutopilotIO Interactables;
    //Indice relativo alla soluzione scelta in data.Fase#
    //I due indici dell'array sono le due possibili fasi.
    int[] solutionCombinantion = new int[2];

    int _currentSolIndex;
    int currentSolutionIndex
    {
        get { return _currentSolIndex; }
        set
        {
            _currentSolIndex = value;
            OnCurrentSolutionIndexUpdate();
        }
    }
    bool isFase1Solved;
    bool isFase2Solved;

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

    public void OnButtonSelect(SelectableButton _button)
    {
        InputValue value = (_button.InputData as PuzzleAutopilotInputData).Actualvalue;
        CompareInputWithSolution(value);
    }
    public void OnSwitchSelect(SelectableSwitch _switch)
    {
        InputValue value = (_switch.InputData as PuzzleAutopilotInputData).Actualvalue;
        CompareInputWithSolution(value);
    }
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

    protected override void OnInitEnd(SelectableAbstract _parent)
    {
        //Crea un setupIniziale;
        GenerateInitialValues();
        InitSwitches();
        InitButtons();
        InitOutputMonitors();

        //Condizioni iniziali di partita
        currentSolutionIndex = 0;
        isFase1Solved = false;
        isFase2Solved = false;
    }

    void InitSwitches()
    {
        InputValue randDx = (InputValue)Random.Range(-2, 0);
        InputValue randSx = (InputValue)Random.Range(-4, -2);

        Interactables.LevaDx.Init(this);
        Interactables.LevaDx.DataInjection(new PuzzleAutopilotInputData() { Actualvalue = randDx });
        Interactables.LevaDx.selectStatus = (int)randDx % 2 == 0 ? true : false;

        Interactables.LevaSx.Init(this);
        Interactables.LevaSx.DataInjection(new PuzzleAutopilotInputData() { Actualvalue = randSx });
        Interactables.LevaSx.selectStatus = (int)randSx % 2 == 0 ? true : false;
    }

    void InitButtons() {

        for (int i = 0; i < 6; i++)
        {
            switch (i)
            {
                case 0:
                    Interactables.ButtonA.DataInjection(new PuzzleAutopilotInputData() { Actualvalue = InputValue.BottoneA });
                    Interactables.ButtonA.Init(this);
                    break;
                case 1:
                    Interactables.ButtonB.DataInjection(new PuzzleAutopilotInputData() { Actualvalue = InputValue.BottoneB });
                    Interactables.ButtonB.Init(this);
                    break;
                case 2:
                    Interactables.ButtonF.DataInjection(new PuzzleAutopilotInputData() { Actualvalue = InputValue.BottoneF });
                    Interactables.ButtonF.Init(this);
                    break;
                case 3:
                    Interactables.ButtonG.DataInjection(new PuzzleAutopilotInputData() { Actualvalue = InputValue.BottoneG });
                    Interactables.ButtonG.Init(this);
                    break;
                case 4:
                    Interactables.ButtonK.DataInjection(new PuzzleAutopilotInputData() { Actualvalue = InputValue.BottoneK });
                    Interactables.ButtonK.Init(this);
                    break;
                case 5:
                    Interactables.ButtonL.DataInjection(new PuzzleAutopilotInputData() { Actualvalue = InputValue.BottoneL });
                    Interactables.ButtonL.Init(this);
                    break;
                default:
                    break;
            }
        }
    }

    void InitOutputMonitors()
    {
        OutputValue output = Data.Fase1[solutionCombinantion[0]].MonitorOutput;
        Interactables.MonitorFase1.SetMaterial((int)output);

        OutputValue output2 = Data.Fase2[solutionCombinantion[1]].MonitorOutput;
        Interactables.MonitorFase2.SetMaterial((int)output2);

        Interactables.MonitorFase2.ToggleOnOff(false);
        Interactables.MonitorFaseOK.ToggleOnOff(false);
    }

    //genera a caso una combinazione iniziale del puzzle (e la soluzione)
    void GenerateInitialValues() {
        int fase1index = Random.Range(0, Data.Fase1.Count);
        solutionCombinantion[0] = fase1index;

        int fase2index = Random.Range(0, Data.Fase2.Count);
        solutionCombinantion[1] = fase2index;
    }

    void DoWinningthings() { SolutionState = PuzzleState.Solved; }
    void DoBreakingThings() { SolutionState = PuzzleState.Broken; }

    bool CompareInputWithSolution(InputValue _input)
    {
        List<InputValue> combToCompare = new List<InputValue>();
        if (!isFase1Solved)
            combToCompare = Data.Fase1[solutionCombinantion[0]].Solution;
        else if(!isFase2Solved)
            combToCompare = Data.Fase2[solutionCombinantion[1]].Solution;

        if(combToCompare[currentSolutionIndex] == _input)
        {
            currentSolutionIndex++;
            return true;
        }
        else
        {
            currentSolutionIndex = 0;
            DoBreakingThings();
            return false;
        }
    }

    void OnCurrentSolutionIndexUpdate()
    {
        //Get the actual combination
        List<InputValue> combToCompare = new List<InputValue>();
        if (!isFase1Solved)
            combToCompare = Data.Fase1[solutionCombinantion[0]].Solution;
        else if (!isFase2Solved)
            combToCompare = Data.Fase2[solutionCombinantion[1]].Solution;

        //Compare eventual already done lever status
        InputValue currentInput = combToCompare[currentSolutionIndex];
        InputValue leverValue;
        if (currentSolutionIndex < -2)
        {
            leverValue = (Interactables.LevaSx.InputData as PuzzleAutopilotInputData).Actualvalue;
            if(leverValue == currentInput)
            {
                currentSolutionIndex++;
            }
        }
        else if(currentSolutionIndex < 0)
        {
            leverValue = (Interactables.LevaDx.InputData as PuzzleAutopilotInputData).Actualvalue;
            if (leverValue == currentInput)
            {
                currentSolutionIndex++;
            }
        }

        //Check if fase is solved
        if(currentSolutionIndex >= combToCompare.Count)
        {
            if (!isFase1Solved)
                isFase1Solved = true;
            else if (!isFase2Solved)
            {
                isFase2Solved = true;
                DoWinningthings();
            }
        }
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
        LevaSx_Sx = -4,
        LevaSx_Dx,
        LevaDx_Sx,
        LevaDx_Dx,
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

