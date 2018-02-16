using System.Collections.Generic;
using UnityEngine;

public class SelectionRoot : SelectableItem
{
    CameraController camCtrl;
    public List<SelectableGeneric> puzzles = new List<SelectableGeneric>();

    private void Start()
    {
        camCtrl = Camera.main.GetComponent<CameraController>();
        foreach (SelectableGeneric puzzle in puzzles)
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
