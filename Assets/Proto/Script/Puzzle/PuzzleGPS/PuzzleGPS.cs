using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGPS : SelectableItem, IPuzzle
{
    public PuzzleGPSData Data;
    public GPS_IO Interactables;

    PuzzleState _solutionState = PuzzleState.Unsolved;
    public PuzzleState SolutionState
    {
        get
        {
            return _solutionState;
        }

        set
        {
            _solutionState = value;
        }
    }

    public void Setup(IPuzzleData _data)
    {
        Data = _data as PuzzleGPSData;
    }

    void InitNumerics()
    {
        for (int i = 0; i < Interactables.NumericalButtons.Length; i++)
        {
            Interactables.NumericalButtons[i].specificBehaviour = new PuzzleGPSNumeric(i);
            Interactables.NumericalButtons[i].Init(this);
        }
    }

    void InitSelectableMonitors()
    {

    }

    [System.Serializable]
    public class GPS_IO
    {
        public SelectableButton[] NumericalButtons = new SelectableButton[12];
    }
}
