using UnityEngine;

/// <summary>
/// Behaviour that all puzzles share
/// </summary>
public interface IPuzzle {

    PuzzleState SolutionState { get; set; }

    void Setup(IPuzzleData data);

    void DoWin();
    void DoLoose();
    bool CheckIfSolved();

    void OnButtonSelect(SelectableButton _button);
    void OnSwitchSelect(SelectableSwitch _switch);
    void OnMonitorSelect(SelectableMonitor _monitor);
    void OnUpdateSelectable(SelectableAbstract _selectable);
}

/// <summary>
/// Interface used to flag all the data needed by an IPuzzle
/// Necessary to impose a Data injection on Setup and avid generic behaviours
/// </summary>
public interface IPuzzleData {
    GameObject GetIPuzzleGO();
}
public interface IPuzzleInputData { }

public enum PuzzleState
{
    Unsolved,
    Broken,
    Solved
}

