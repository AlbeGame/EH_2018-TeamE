using UnityEngine;

public class PuzzleContainer : SelectableItem
{
    public Transform CamFocusPosition;

    CameraController camCtrl;
    ItemGraphicController graphicCtrl;

    protected override void OnInit(SelectionManager _selectMng)
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
