using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class that share the common behaviour of selectable items:
/// Switch states and can be "Selected".
/// Have to be injected on Init() with a SelectionManager
/// </summary>
public abstract class SelectableAbstract : MonoBehaviour
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
    public SelectableAbstract Parent { get; private set; }
    SelectableAbstract _selectedScion;
    public SelectableAbstract SelectedScion
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
    public List<SelectableAbstract> Children = new List<SelectableAbstract>();
    public List<SelectableAbstract> Siblings
    {
        get
        {
            if (Parent)
                return GetSiblings();
            else
                return new List<SelectableAbstract>();
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
                foreach (SelectableAbstract child in Children)
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
                foreach (SelectableAbstract child in Children)
                    child.State = SelectionState.Neutral;
                break;
            case SelectionState.Passive:
                if (hasASelectedChild)
                {
                    foreach (SelectableAbstract sibling in SelectedScion.GetSiblings())
                        sibling.State = SelectionState.Neutral;
                }
                else
                {
                    foreach (SelectableAbstract child in Children)
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
    protected List<SelectableAbstract> GetSiblings()
    {
        if (Parent == null)
            return new List<SelectableAbstract>();

        //TODO: lista da salvare e aggiornare ad ogni cambio di "children" per ottimizzare gli spazi
        List<SelectableAbstract> siblings = new List<SelectableAbstract>();
        foreach (SelectableAbstract child in Parent.Children)
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
    protected SelectableAbstract GetRoot()
    {
        SelectableAbstract root = this;
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
    protected SelectableAbstract GetParent()
    {
        for (Transform p = transform.parent; p != null; p = p.parent)
        {
            SelectableAbstract firstParentFound = p.GetComponent<SelectableAbstract>();
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
    public void Init(SelectableAbstract _parent, SelectionState _state = SelectionState.Neutral)
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
        SelectableAbstract parent;
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
    protected virtual void OnInitBegin(SelectableAbstract _parent) { }
    protected virtual void OnInitEnd(SelectableAbstract _parent) { }
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
