using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleTurbine : SelectableGeneric, IPuzzle
{
    public PuzzleTurbineData Data;
    PuzzleCombination combination;
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

    protected override void OnInitEnd(SelectableItem _parent)
    {
        GenerateNewPuzzleCombination();
        InitGenricalElement();
    }

    public void Setup(IPuzzleData _data)
    {
        Data = _data as PuzzleTurbineData;
        InitGenricalElement();
    }


    private void InitGenricalElement()
    {
        List<TurbineButtonData> buttonPool = new List<TurbineButtonData>();
        foreach (var item in combination.Solution)
            buttonPool.Add(item);
        foreach (var item in combination.Fillers)
            buttonPool.Add(item);

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
                    TurbineButtonData buttonData = buttonPool[Random.Range(0, buttonPool.Count)];
                    buttonPool.Remove(buttonData);
                    button.specificBehaviour = new PuzzleTurbineButtonTagged(this, buttonData);
                    button.Init();
                    break;
                default:
                    break;
            }
        }

        Data.EValues = combination.EValues;
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

    private void GenerateNewPuzzleCombination()
    {
        PuzzleCombination newComb = new PuzzleCombination();
        TurbineButtonData possibleButton;
        List<TurbineButtonData> usedButtons = new List<TurbineButtonData>();
        //Numero di pulsanti richiesti;
        for (int i = 0; i < 5; i++)
        {
            if (i < 2)
                while (newComb.Solution.Count == i)
                {
                    possibleButton = GetUnchosenButton(usedButtons);
                    if (IsSolvable(newComb, possibleButton))
                    {
                        newComb.Solution.Add(possibleButton);
                        usedButtons.Add(possibleButton);
                        break;
                    }
                }
            else
            {
                possibleButton = GetUnchosenButton(usedButtons);
                usedButtons.Add(possibleButton);
                newComb.Fillers.Add(possibleButton);
            }
        }

        combination = newComb;
    }
    TurbineButtonData GetUnchosenButton(List<TurbineButtonData> alreadyChosen)
    {
        List<TurbineButtonData> possibles = Data.ButtonsValues.ToList();
        foreach (var item in alreadyChosen)
            possibles.Remove(item);

        int chosenIndex = Random.Range(0, possibles.Count);

        return possibles[chosenIndex];
    }
    bool IsSolvable(PuzzleCombination _combination, TurbineButtonData newButton)
    {
        int[] currentEs = _combination.EValues;

        currentEs[0] -= newButton.E1Modifier;
        if (currentEs[0] < 0 || currentEs[0] > 100)
            return false;
        currentEs[1] -= newButton.E2Modifier;
        if (currentEs[1] < 0 || currentEs[1] > 100)
            return false;
        currentEs[2] -= newButton.E3Modifier;
        if (currentEs[2] < 0 || currentEs[2] > 100)
            return false;
        currentEs[3] -= newButton.E4Modifier;
        if (currentEs[3] < 0 || currentEs[3] > 100)
            return false;

        return true;
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

    /// <summary>
    /// Values setup of the puzzle.
    /// </summary>
    class PuzzleCombination
    {
        public int[] EValues {get { return GetEs(); }}
        public List<TurbineButtonData> Solution = new List<TurbineButtonData>();
        public List<TurbineButtonData> Fillers = new List<TurbineButtonData>();
        //Assuming solution is correct
        int[] GetEs()
        {
            int[] eS = new int[] { 50, 50, 50, 50 };

            foreach (var sol in Solution)
            {
                eS[0] -= sol.E1Modifier;
                eS[1] -= sol.E2Modifier;
                eS[2] -= sol.E3Modifier;
                eS[3] -= sol.E4Modifier;
            }

            return eS;
        }
    }
}
