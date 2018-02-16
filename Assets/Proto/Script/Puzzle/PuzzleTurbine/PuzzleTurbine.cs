using System.Collections.Generic;
using UnityEngine;

public class PuzzleTurbine : SelectableGeneric, IPuzzle
{
    public PuzzleTurbineData Data;

    SelectableButton ResetButton;
    List<SliderController> Sliders = new List<SliderController>();

    PuzzleState _solutionState = PuzzleState.Unsolved;
    public PuzzleState SolutionState
    {
        get { return _solutionState; }
        set
        {
            if (SolutionState == value)
                return;

            _solutionState = value;
            OnSolutionStateChange(SolutionState);
        }
    }

    protected override void OnStartEnd()
    {
        InitPrivate();
    }

    public void Setup(IPuzzleData _data) {

        Data = _data as PuzzleTurbineData;
        InitPrivate();
    }

    private void InitPrivate()
    {
        foreach (SelectableButton button in GetComponentsInChildren<SelectableButton>())
        {
            if (button.Puzzle != PuzzleType.Turbine)
                continue;

            switch (button.Type)
            {
                case ButtonType.Untagged:
                    ResetButton = button;
                    break;
                case ButtonType.Tagged:
                    button.specificBehaviour = new PuzzleTurbineButtonTagged(this);
                    button.Init();
                    break;
                default:
                    break;
            }
        }

        foreach (SliderController slider in GetComponentsInChildren<SliderController>())
        {
            Sliders.Add(slider);
        }
        UpdateSliderValues();
    }

    public void SetEValues(int E1, int E2, int E3, int E4)
    {
        Data.EValues[0] += E1;
        Data.EValues[1] += E2;
        Data.EValues[2] += E3;
        Data.EValues[3] += E4;

        for (int i = 0; i < Data.EValues.Length; i++)
        {
            if (Data.EValues[i] < 0)
                Data.EValues[i] = 0;
            if (Data.EValues[i] > 100)
                Data.EValues[i] = 100;
        }

        UpdateSliderValues();
        CheckBreackDown();
    }
    public void CheckSolution()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Data.EValues[i] >= 70 && Data.EValues[i] <= 80)
                continue;

            DoBreakThings();
            return;
        }

        DoWinningThings();
    }

    void CheckBreackDown()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Data.EValues[i] <= 0 && Data.EValues[i] >= 100)
                DoBreakThings();
        }
    }

    void DoWinningThings()
    {

        //foreach (var item in TaggedButtons)
        //{
        //    item.GetComponent<MeshCollider>().enabled = false;
        //}
        //ResetButton.GetComponent<MeshCollider>().enabled = false;

        Parent.Select(true);
        SolutionState = PuzzleState.Solved;
        State = SelectionState.Unselectable;
    }

    void DoBreakThings()
    {
        SolutionState = PuzzleState.Broken;
    }

    void UpdateSliderValues()
    {
        for (int i = 0; i < Sliders.Count; i++)
        {
            Sliders[i].SetFillAmount(Data.EValues[i]);
        }
    }

    void OnSolutionStateChange(PuzzleState _solutionState)
    {
        graphicCtrl.Paint(_solutionState);
    }
}
