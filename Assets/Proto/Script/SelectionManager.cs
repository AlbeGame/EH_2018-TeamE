using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    List<SelectableItem> selectables = new List<SelectableItem>();

    SelectableItem _currentSelected;
    public SelectableItem CurrentSelected
    {
        get { return _currentSelected; }
        set
        {
            if (CurrentSelected == value)
                return;

            OnCurrentSelectedChange(value, _currentSelected);
            _currentSelected = value;
        }
    }

    CameraController camCtrl;

    private void Start()
    {
        camCtrl = Camera.main.GetComponent<CameraController>();
        //TODO: da rimuovere quando verrà creato un sistema di ceazione dinamica dei puzzle
        foreach (SelectableItem selectItem in FindObjectsOfType<SelectableItem>())
        {
            selectItem.Init(this);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
            if(!CurrentSelected.HasMouseOver)
                CurrentSelected = null;
    }

    void OnCurrentSelectedChange(SelectableItem _newSel, SelectableItem _oldSel)
    {
        if (_oldSel != null)
            _oldSel.State = SelectionState.Normal;

        if (_newSel == null)
            camCtrl.FocusReset();
    }

    #region API
    public void AddSelectable(SelectableItem _selectable)
    {
        selectables.Add(_selectable);
    }
    #endregion
}
