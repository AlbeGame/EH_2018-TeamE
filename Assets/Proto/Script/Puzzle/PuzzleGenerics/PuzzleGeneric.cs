using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemGraphicController))]
public class PuzzleGeneric : SelectableItem
{
    public Transform CamFocusPosition;
    CameraController camCtrl;
    ItemGraphicController graphicCtrl;

    PuzzleState _solutionState = PuzzleState.Unsolved;
    protected PuzzleState SolutionState
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
    
    void Start()
    {
        camCtrl = Camera.main.GetComponent<CameraController>();
        graphicCtrl = GetComponent<ItemGraphicController>();
        OnStartEnd();
    }

    protected override void OnSelect()
    {
        if (CamFocusPosition)
            camCtrl.FocusAt(CamFocusPosition);
    }

    protected override void OnStateChange(SelectionState _state)
    {
        if (graphicCtrl)
            graphicCtrl.Paint(_state);

    }

    protected virtual void OnSolutionStateChange(PuzzleState _newState)
    {
        graphicCtrl.Paint(_newState);
    }

    protected virtual void OnStartEnd() { }
}

public enum PuzzleState
{
    Unsolved,
    Broken,
    Solved
}
