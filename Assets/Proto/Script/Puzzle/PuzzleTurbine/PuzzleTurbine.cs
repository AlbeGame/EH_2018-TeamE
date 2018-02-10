using System.Collections.Generic;
using UnityEngine;

public class PuzzleTurbine : PuzzleGeneric
{
    public Vector4 EVales = new Vector4(30, 70, 20, 90);

    public Puzzle1ButtonReset ResetButton;
    public List<Puzzle1ButtonTagged> TaggedButtons = new List<Puzzle1ButtonTagged>();
    public List<SliderController> Sliders = new List<SliderController>();


    protected override void OnStartEnd()
    {
        ResetButton = GetComponentInChildren<Puzzle1ButtonReset>();
        ResetButton.puzzleCtrl = this;

        foreach (Puzzle1ButtonTagged button in GetComponentsInChildren<Puzzle1ButtonTagged>())
        {
            TaggedButtons.Add(button);
            button.puzzleCtrl = this;
        }
        foreach (SliderController slider in GetComponentsInChildren<SliderController>())
        {
            Sliders.Add(slider);
        }
        UpdateSliderValues();
    }

    public void SetEValues(Vector4 _eValues)
    {
        EVales += _eValues;
        for (int i = 0; i < 4; i++)
        {
            if (EVales[i] < 0)
                EVales[i] = 0;
            if (EVales[i] > 100)
                EVales[i] = 100;
        }

        UpdateSliderValues();
        CheckBreackDown();
    }

    public void CheckSolution()
    {
        for (int i = 0; i < 4; i++)
        {
            if (EVales[i] >= 70 && EVales[i] <= 80)
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
            if (EVales[i] <= 0 && EVales[i] >= 100)
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
            Sliders[i].SetFillAmount(EVales[i]);
        }
    }
}
