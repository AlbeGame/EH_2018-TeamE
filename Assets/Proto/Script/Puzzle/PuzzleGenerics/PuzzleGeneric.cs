using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGeneric : SelectableItem
{
    public PuzzleGraphicData GraphicData;
    PuzzleGraphic graphicCtrl;

    public PuzzleInteractionData InteractionData;
    PuzzleInteraction interactionCtrl;

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
        //Graphic Controller
        graphicCtrl = GetComponent<PuzzleGraphic>();
        if (graphicCtrl == null)
            graphicCtrl = gameObject.AddComponent<PuzzleGraphic>();
        graphicCtrl.Init(GraphicData);

        //Interaction Controller
        interactionCtrl = GetComponent<PuzzleInteraction>();
        if (interactionCtrl == null)
            interactionCtrl = gameObject.AddComponent<PuzzleInteraction>();
        interactionCtrl.Init(InteractionData);

        OnStartEnd();
    }

    protected override void OnSelect()
    {
        interactionCtrl.CameraFocusCall();
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
