using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class that share the common behaviour of selectable items:
/// Switch states and can be "Selected".
/// Have to be injected on Init() with a SelectionManager
/// </summary>
public abstract class SelectableItem : MonoBehaviour
{
    //State property
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

    //Extended component pattern variables
    public SelectableItem Parent { get; private set; }
    SelectableItem _selectedScion;
    public SelectableItem SelectedScion
    {
        get { return _selectedScion; }
        set {
            if (value == SelectedScion)
                return;
            SelectableItem _oldScion = _selectedScion;
            _selectedScion = value;
            OnSelectedScionSet(SelectedScion, _oldScion);
        }
    }
    public List<SelectableItem> Children = new List<SelectableItem>();
    public List<SelectableItem> Siblings { get { return Parent.GetSiblings(this); } }
    
    //not state dependend conditionals
    public bool HasMouseOver { get; protected set; }
    protected bool hasASelectedChild { get { return Children.Contains(SelectedScion); } }



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

    /// <summary>
    /// Called on in State property
    /// </summary>
    /// <param name="_state"></param>
    void OnStateSwitch(SelectionState _state)
    {
        switch (State)
        {
            case SelectionState.Passive:
                if (hasASelectedChild)
                {
                    foreach (SelectableItem sibling in GetSiblings(SelectedScion))
                    {
                        sibling.State = SelectionState.Normal;
                    }
                }
                else {
                    foreach (SelectableItem child in Children)
                    {
                        child.State = SelectionState.Passive;
                    }
                }
                break;
            case SelectionState.Normal:
                foreach (SelectableItem child in Children)
                {
                    child.State = SelectionState.Passive;
                }
                break;
            case SelectionState.Highlighted:
                break;
            case SelectionState.Selected:
                if (Parent)
                {
                    Parent.SelectedScion = this;
                    Parent.State = SelectionState.Passive;
                }

                foreach (SelectableItem child in Children)
                {
                    child.State = SelectionState.Normal;
                }
                break;
        }

        OnStateChange(_state);
    }
    /// <summary>
    /// Reaction on SelectedScion set
    /// </summary>
    /// <param name="_newScion"></param>
    /// <param name="_oldScion"></param>
    void OnSelectedScionSet(SelectableItem _newScion, SelectableItem _oldScion) {
        if (Parent)
        {
            Parent.SelectedScion = _newScion;
        }
        if (_oldScion)
        {
            _oldScion.State = SelectionState.Passive;
        }
    }
    /// <summary>
    /// Return all the children except _child
    /// </summary>
    /// <param name="_child"></param>
    /// <returns></returns>
    private List<SelectableItem> GetSiblings(SelectableItem _child)
    {
        if (Parent == null)
            return new List<SelectableItem>();

        //TODO: lista da salvare e aggiornare ad ogni cambio di "children" per ottimizzare gli spazi
        List<SelectableItem> siblings = new List<SelectableItem>();
        foreach (SelectableItem child in Parent.Children)
        {
            if (child != this)
                siblings.Add(child);
        }

        return siblings;
    }

    #region API
    /// <summary>
    /// Initialize the class
    /// </summary>
    /// <param name="_parent"></param>
    public void Init(SelectableItem _parent, SelectionState _state = SelectionState.Normal)
    {
        OnInitBegin(_parent);

        if (_parent)
        {
            Parent = _parent;
            Parent.Children.Add(this);
        }

        State = _state;

        OnInitEnd(_parent);
    }
    /// <summary>
    /// Call the reaction on selection;
    /// </summary>
    public void Select()
    {
        State = SelectionState.Selected;
        OnSelect();
    }
    #endregion
    
    #region Virtual Methods
    protected virtual void OnInitBegin(SelectableItem _parent) { }
    protected virtual void OnInitEnd(SelectableItem _parent) { }
    protected virtual void OnStateChange(SelectionState _newState) { }
    protected virtual void OnSelect() { }
    #endregion
}

public enum SelectionState
{
    Passive = -1,
    Normal,
    Highlighted,
    Selected
}
