using UnityEngine;

public class PuzzleContainer : SelectableItem
{
    public Transform CamFocusPosition;
    CameraController camCtrl;
    ItemGraphicController graphicCtrl;

    public void SpegniStoCoso()
    {
        graphicCtrl = null;
        enabled = false;
    }

    protected override void OnInitEnd(SelectableItem _parent)
    {
        camCtrl = Camera.main.GetComponent<CameraController>();
        graphicCtrl = GetComponentInChildren<ItemGraphicController>();
    }

    protected override void OnSelect()
    {
        if (CamFocusPosition)
            camCtrl.FocusAt(CamFocusPosition);
    }

    protected override void OnStateChange(SelectionState _state)
    {
        switch (_state)
        {
            case SelectionState.Neutral:
                if (graphicCtrl)
                    graphicCtrl.PaintNormal();
                break;
            case SelectionState.Highlighted:
                if (graphicCtrl)
                    graphicCtrl.PaintHighlight();
                break;
            case SelectionState.Selected:
                if (graphicCtrl)
                    graphicCtrl.PaintPressed();
                break;
        }
    }
}
