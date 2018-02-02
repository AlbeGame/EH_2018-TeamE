using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    List<ISelectable> selectables = new List<ISelectable>();

    ISelectable _currentSelected;
    public ISelectable currentSelected
    {
        get { return _currentSelected; }
        set
        {
            if (currentSelected == value)
                return;

            OnCurrentSelectedChange(value, _currentSelected);
            _currentSelected = value;
        }
    }

    CameraController camCtrl;

    private void Start()
    {
        camCtrl = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
            currentSelected = null;
    }

    void OnCurrentSelectedChange(ISelectable _newSel, ISelectable _oldSel)
    {
        if (_oldSel != null)
            _oldSel.State = SelectionState.Normal;

        if (_newSel == null)
            camCtrl.FocusReset();
    }

    #region API
    public void AddSelectable(ISelectable _selectable)
    {
        selectables.Add(_selectable);
    }
    #endregion
}
