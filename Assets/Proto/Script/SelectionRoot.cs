using System.Collections.Generic;
using UnityEngine;

public class SelectionRoot : SelectableItem
{
    CameraController camCtrl;
    public List<PuzzleContainer> puzzles = new List<PuzzleContainer>();

    private void Start()
    {
        camCtrl = Camera.main.GetComponent<CameraController>();
        foreach (PuzzleContainer puzzle in puzzles)
        {
            puzzle.Init(this, SelectionState.Normal);
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
