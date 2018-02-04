using UnityEngine;

public class PuzzleContainer : SelectableItem
{
    public Transform CamFocusPosition;
    Puzzle1Controller PuzzleController;
    CameraController camCtrl;
    ItemGraphicController graphicCtrl;

    public void SpegniStoCoso()
    {
        graphicCtrl = null;
        enabled = false;
    }

    protected override void OnInit(SelectionManager _selectMng)
    {
        PuzzleController = GetComponentInChildren<Puzzle1Controller>();
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
            case SelectionState.Normal:
                if (graphicCtrl)
                    graphicCtrl.PaintNormal();
                break;
            case SelectionState.Highlighted:
                if (graphicCtrl)
                    graphicCtrl.PaintHighlight();
                break;
            case SelectionState.Pressed:
                if (graphicCtrl)
                    graphicCtrl.PaintPressed();
                break;
        }
    }
}
