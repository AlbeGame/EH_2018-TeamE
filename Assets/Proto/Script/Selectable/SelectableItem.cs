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
            _state = value;
            OnStateSwitch(State);
        }
    }

    //Extended component pattern variables
    public SelectableItem Parent { get; private set; }
    SelectableItem _selectedScion;
    public SelectableItem SelectedScion
    {
        get {
            if (Parent)
                return GetRoot().SelectedScion;
            else
                return _selectedScion;
        }
        set
        {
            if (value == SelectedScion)
                return;

            _selectedScion = value;
        }
    }
    [HideInInspector]
    public List<SelectableItem> Children = new List<SelectableItem>();
    public List<SelectableItem> Siblings
    {
        get
        {
            if (Parent)
                return GetSiblings();
            else
                return new List<SelectableItem>();
        }
    }

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

        if (State == SelectionState.Neutral)
            State = SelectionState.Highlighted;
    }
    private void OnMouseExit()
    {
        HasMouseOver = false;

        if (State == SelectionState.Highlighted)
            State = SelectionState.Neutral;
    }

    /// <summary>
    /// Called on in State property
    /// </summary>
    /// <param name="_state"></param>
    void OnStateSwitch(SelectionState _state)
    {
        switch (_state)
        {
            case SelectionState.Unselectable:
                Parent.Children.Remove(this);
                break;
            case SelectionState.Neutral:
                foreach (SelectableItem child in Children)
                    child.State = SelectionState.Passive;
                break;
            case SelectionState.Highlighted:
                break;
            case SelectionState.Selected:
                //No one can be selected without being the SelectedScion of itself
                GetRoot().SelectedScion = this;
                if (Parent != null)
                    GetRoot().State = SelectionState.Passive;
                //Give the parent this information
                //if (Parent)
                //    Parent.SelectedScion = this;
                //Unlock neutral state for children
                foreach (SelectableItem child in Children)
                    child.State = SelectionState.Neutral;
                break;
            case SelectionState.Passive:
                if (hasASelectedChild)
                {
                    foreach (SelectableItem sibling in SelectedScion.GetSiblings())
                        sibling.State = SelectionState.Neutral;
                }
                else
                {
                    foreach (SelectableItem child in Children)
                        child.State = SelectionState.Passive;
                }
                break;
        }

        OnStateChange(_state);
    }

    /// <summary>
    /// Return all the children except _child
    /// </summary>
    /// <param name="_child"></param>
    /// <returns></returns>
    protected List<SelectableItem> GetSiblings()
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
    /// <summary>
    /// Return the Root
    /// </summary>
    /// <returns></returns>
    protected SelectableItem GetRoot()
    {
        SelectableItem root = this;
        while (root.Parent)
        {
            root = root.Parent;
        }

        return root;
    }
    /// <summary>
    /// Return the parent of this SelectableItem (climbing the transform hierarchy
    /// </summary>
    /// <returns></returns>
    protected SelectableItem GetParent()
    {
        for (Transform p = transform.parent; p != null; p = p.parent)
        {
            SelectableItem firstParentFound = p.GetComponent<SelectableItem>();
            if (firstParentFound != null)
            {
                return firstParentFound;
            }
        }

        return null;
    }

    #region API
    /// <summary>
    /// Initialize the class
    /// </summary>
    /// <param name="_parent"></param>
    public void Init(SelectableItem _parent, SelectionState _state = SelectionState.Neutral)
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
    /// Initialize the class
    /// </summary>
    /// <param name="_parent"></param>
    public void Init(bool _searchParent = true, SelectionState _state = SelectionState.Neutral)
    {
        SelectableItem parent;
        if (_searchParent)
            parent = GetParent();
        else
            parent = null;

        OnInitBegin(parent);


        if (parent)
        {
            Parent = parent;
            Parent.Children.Add(this);
        }

        State = _state;

        OnInitEnd(parent);
    }

    /// <summary>
    /// Call the reaction on selection;
    /// </summary>
    public void Select(bool ignoreState = false)
    {
        if (State == SelectionState.Unselectable)
            return;

        if (ignoreState || State != SelectionState.Passive)
        {
            State = SelectionState.Selected;
            OnSelect();
        }
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
    Unselectable = -1,
    Neutral,
    Highlighted,
    Selected,
    Passive
}
