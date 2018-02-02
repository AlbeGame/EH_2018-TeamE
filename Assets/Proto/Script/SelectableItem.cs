using UnityEngine;

public class SelectableItem : MonoBehaviour, ISelectable
{

    public Transform CamFocusPosition;

    SelectionState _state;
    public SelectionState State
    {
        get
        {
            return _state;
        }

        set
        {
            if (State == value)
                return;

            _state = value;
            OnStateChange(State);
        }
    }

    ItemGraphicController graphicCtrl;
    SelectionManager selectMng;
    CameraController camCtrl;

    private void Start()
    {
        Init(FindObjectOfType<SelectionManager>());
        camCtrl = Camera.main.GetComponent<CameraController>();
        graphicCtrl = GetComponentInChildren<ItemGraphicController>();

        State = SelectionState.Normal;
    }

    private void OnMouseUpAsButton()
    {
        Select();
    }

    private void OnMouseEnter()
    {
        if (State == SelectionState.Normal)
            State = SelectionState.Highlighted;
    }

    private void OnMouseExit()
    {
        if (State == SelectionState.Highlighted)
            State = SelectionState.Normal;
    }

    void OnStateChange(SelectionState _state)
    {
        if (State == SelectionState.Pressed)
            selectMng.currentSelected = this;

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

    #region API
    public void Init(SelectionManager _selectMng)
    {
        selectMng = _selectMng;
        selectMng.AddSelectable(this);
    }

    public void Select()
    {
        State = SelectionState.Pressed;
        camCtrl.FocusAt(CamFocusPosition);
    }
    #endregion
}
