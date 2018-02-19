using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableGeneric : SelectableItem
{
    public PuzzleGraphicData GraphicData;
    protected PuzzleGraphic graphicCtrl;

    public PuzzleInteractionData InteractionData;
    protected PuzzleInteraction interactionCtrl;

    protected override void OnInitBegin(SelectableItem _parent)
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
}

public enum PuzzleType
{
    Turbine,
    GPS
}
