using UnityEngine;

public class ButtonReset : SelectableItem {

    public Puzzle1Controller puzzleCtrl;
    public GameObject ButtonGraphic;

    protected override void OnSelect()
    {
        if (puzzleCtrl != null)
            puzzleCtrl.CheckSolution();
    }
}
