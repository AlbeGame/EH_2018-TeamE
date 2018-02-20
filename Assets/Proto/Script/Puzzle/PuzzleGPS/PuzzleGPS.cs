using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGPS : SelectableItem, IPuzzle
{
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

    public void Setup(IPuzzleData data)
    {
        throw new System.NotImplementedException();
    }
}
