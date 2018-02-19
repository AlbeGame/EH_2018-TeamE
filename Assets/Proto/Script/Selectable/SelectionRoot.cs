using System.Collections.Generic;
using UnityEngine;

public class SelectionRoot : SelectableAbstract
{
    CameraController camCtrl;
    public List<SelectableItem> Selectables = new List<SelectableItem>();

    private void Start()
    {
        camCtrl = Camera.main.GetComponent<CameraController>();
        foreach (SelectableItem puzzle in Selectables)
        {
            puzzle.Init(this, SelectionState.Neutral);
        }
        State = SelectionState.Selected;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
            if(hasASelectedChild)
                Select(true);
    }

    protected override void OnSelect()
    {
        camCtrl.FocusReset();
    }
}
