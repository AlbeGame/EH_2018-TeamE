using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleTurbine : SelectableItem, IPuzzle
{
    public PuzzleTurbineData Data;
    PuzzleCombination combination;
    List<SliderController> Sliders = new List<SliderController>();

    #region IPuzzle
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

    public void Setup(IPuzzleData _data)
    {
        Data = _data as PuzzleTurbineData;
        InitGenricalElement();
    }
    #endregion

    #region Selectable Behaviours
    protected override void OnInitEnd(SelectableAbstract _parent)
    {
        GenerateNewPuzzleCombination();
        InitGenricalElement();
    }

    void OnSolutionStateChange(PuzzleState _solutionState)
    {
        graphicCtrl.Paint(_solutionState);
    }

    protected override void OnStateChange(SelectionState _state)
    {

        if (graphicCtrl && SolutionState == PuzzleState.Unsolved)
            graphicCtrl.Paint(_state);
    }
    #endregion

    #region Setup and Init specific
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

        newComb.ResetEValues();
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
        int[] currentEs = _combination.InitialEValues;

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
                    button.specificBehaviour = new PuzzleTurbineButtonReset(this);
                    button.Init();
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

        foreach (SliderController slider in GetComponentsInChildren<SliderController>())
        {
            Sliders.Add(slider);
        }
        UpdateSliderValues();
    }
    #endregion

    public void SetEValues(int E1, int E2, int E3, int E4)
    {
        combination.CurrentEValues[0] += E1;
        combination.CurrentEValues[1] += E2;
        combination.CurrentEValues[2] += E3;
        combination.CurrentEValues[3] += E4;

        for (int i = 0; i < combination.CurrentEValues.Length; i++)
        {
            if (combination.CurrentEValues[i] < 0)
                combination.CurrentEValues[i] = 0;
            if (combination.CurrentEValues[i] > 100)
                combination.CurrentEValues[i] = 100;
        }

        UpdateSliderValues();
        CheckBreackDown();
    }

    public void CheckSolution()
    {
        for (int i = 0; i < 4; i++)
        {
            if (combination.CurrentEValues[i] == 50)
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
            if (combination.CurrentEValues[i] <= 0 && combination.CurrentEValues[i] >= 100)
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
            Sliders[i].SetFillAmount(combination.CurrentEValues[i]);
        }
    }

    public void OnButtonSelect(SelectableButton _button) { }
    public void OnSwitchSelect(SelectableSwitch _switch) { }
    public void OnMonitorSelect(SelectableMonitor _monitor) { }

    /// <summary>
    /// Values setup of the puzzle.
    /// </summary>
    class PuzzleCombination
    {
        public int[] InitialEValues {get { return GetEs(); }}
        public int[] CurrentEValues { get; private set; }
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

        public void ResetEValues()
        {
            CurrentEValues = InitialEValues;
        }
    }
}
