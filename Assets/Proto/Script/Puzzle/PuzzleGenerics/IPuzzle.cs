using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzle {

    PuzzleState SolutionState { get; set; }

    void Setup(IPuzzleData data);
}

public interface IPuzzleData
{

}

public enum PuzzleState
{
    Unsolved,
    Broken,
    Solved
}