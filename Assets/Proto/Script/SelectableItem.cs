using UnityEngine;

/// <summary>
/// Class that share the common behaviour of selectable items:
/// Switch states and can be "Selected".
/// Have to be injected on Init() with a SelectionManager
/// </summary>
public abstract class SelectableItem : MonoBehaviour
{
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
            OnStateSwitch(State);
        }
    }
    public bool HasMouseOver { get; protected set; }
    public SelectionManager SelectMng { get; private set; }

    private void OnMouseUpAsButton()
    {
        Select();
    }

    private void OnMouseEnter()
    {
        HasMouseOver = true;

        if (State == SelectionState.Normal)
            State = SelectionState.Highlighted;
    }

    private void OnMouseExit()
    {
        HasMouseOver = false;

        if (State == SelectionState.Highlighted)
            State = SelectionState.Normal;
    }

    void OnStateSwitch(SelectionState _state)
    {
        if (State == SelectionState.Pressed)
            SelectMng.CurrentSelected = this;

        OnStateChange(_state);
    }

    #region API
    /// <summary>
    /// Initialize the class
    /// </summary>
    /// <param name="_selectMng"></param>
    public void Init(SelectionManager _selectMng, SelectionState _state = SelectionState.Normal)
    {
        SelectMng = _selectMng;
        SelectMng.AddSelectable(this);
        State = _state;

        OnInit(_selectMng);
    }
    /// <summary>
    /// Initialize the class
    /// </summary>
    /// <param name="_selectMng"></param>
    public void Init(SelectableItem _previousGerarcic, SelectionState _state = SelectionState.Normal)
    {
        SelectMng = _previousGerarcic.SelectMng;
        SelectMng.AddSelectable(this);
        State = _state;

        OnInit(_previousGerarcic);
    }
    /// <summary>
    /// Call the reaction on selection;
    /// </summary>
    public void Select()
    {
        State = SelectionState.Pressed;
        OnSelect();
    }

    protected virtual void OnInit(SelectionManager _selectMng) { }
    protected virtual void OnInit(SelectableItem _previousGerarcic) { }
    protected virtual void OnStateChange(SelectionState _newState) { }
    protected virtual void OnSelect() { }
    #endregion
}

public enum SelectionState
{
    Normal,
    Highlighted,
    Pressed
}
