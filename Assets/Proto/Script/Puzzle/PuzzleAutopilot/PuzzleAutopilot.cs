using System.Collections.Generic;
using UnityEngine;

public class PuzzleAutopilot : SelectableItem, IPuzzle {

    public PuzzleAutopilotData data;
    public List<SelectableButton> ButtonsStandard = new List<SelectableButton>();
    public List<PuzzleAutopilotOutputMonitor> MonitorStandard = new List<PuzzleAutopilotOutputMonitor>();
    public SelectableSwitch LevaSx;
    public SelectableSwitch LevaDx;

    int currentSolutionIndex = 0;
    bool isFase1Completed = false;
    bool isFase2Completed = false;
    int[] currentCombinantion = new int[2];

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
        data = _data as PuzzleAutopilotData;
    }

    public void GetButtonInput(ISelectableBehaviour _selectable) {

        PuzzleAutopilotButton button = _selectable as PuzzleAutopilotButton;

        if (!isFase1Completed) {
            if(data.Fase1[currentCombinantion[0]].Solution[currentSolutionIndex] == button.Actualvalue) {
                currentSolutionIndex++;
                if (currentSolutionIndex > data.Fase1[currentCombinantion[0]].Solution.Count) {
                    isFase1Completed = true;
                    currentSolutionIndex = 0;
                    MonitorStandard[1].ToggleOnOff(true);
                    return;
                }
            }
        } else if (!isFase2Completed) {
            if (data.Fase2[currentCombinantion[0]].Solution[currentSolutionIndex] == button.Actualvalue) {
                currentSolutionIndex++;
                if (currentSolutionIndex > data.Fase2[currentCombinantion[0]].Solution.Count) {
                    isFase2Completed = true;
                    currentSolutionIndex = 0;
                    MonitorStandard[2].ToggleOnOff(true);
                    DoWinningthings();
                    return;
                }
            }
        } 

        DoBadthings();
    }

    void DoWinningthings() { SolutionState = PuzzleState.Solved; }
    void DoBadthings() { SolutionState = PuzzleState.Broken; }

    protected override void OnInitEnd(SelectableAbstract _parent) {
        //Crea un setupIniziale;
        GenerateInitialValues();
        InitButtons();

        string symbol = data.Fase1[currentCombinantion[0]].MonitorSymbol;
        switch (symbol) {
            case "C":
                MonitorStandard[0].SetMaterial(0);
                break;
            case "D":
                MonitorStandard[0].SetMaterial(1);
                break;
            case "E":
                MonitorStandard[0].SetMaterial(2);
                break;
            default:
                break;
        }

        string symbol2 = data.Fase2[currentCombinantion[1]].MonitorSymbol;
        switch (symbol) {
            case "H":
                MonitorStandard[1].SetMaterial(3);
                break;
            case "I":
                MonitorStandard[1].SetMaterial(4);
                break;
            case "J":
                MonitorStandard[1].SetMaterial(5);
                break;
            default:
                break;
        }
        MonitorStandard[1].ToggleOnOff(false);
    }

    void InitButtons() {
        for (int i = 0; i < ButtonsStandard.Count; i++) {
            ButtonsStandard[i].specificBehaviour = new PuzzleAutopilotButton((InputValue)i);
        }
    }

    //genera a caso una combinazione iniziale del puzzle (e la soluzione)
    void GenerateInitialValues() {
        int fase1index = Random.Range(0, data.Fase1.Count);
        currentCombinantion[0] = fase1index;

        int fase2index = Random.Range(0, data.Fase2.Count);
        currentCombinantion[1] = fase2index;
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
}

