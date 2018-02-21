/// <summary>
/// Behaviour that all puzzles share
/// </summary>
public interface IPuzzle {

    PuzzleState SolutionState { get; set; }

    void Setup(IPuzzleData data);

    void OnButtonSelect(SelectableButton _button);
    void OnSwitchSelect(SelectableSwitch _switch);
    void OnMonitorSelect(SelectableMonitor _monitor);
}

/// <summary>
/// Interface used to flag all the data needed by an IPuzzle
/// Necessary to impose a Data injection on Setup and avid generic behaviours
/// </summary>
public interface IPuzzleData { }

public enum PuzzleState
{
    Unsolved,
    Broken,
    Solved
}