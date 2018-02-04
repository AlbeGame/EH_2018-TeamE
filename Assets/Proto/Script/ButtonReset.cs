using UnityEngine;

public class ButtonReset : SelectableItem {

    public Puzzle1Controller puzzleCtrl;
    public GameObject ButtonGraphic;

    protected override void OnInit(SelectableItem _previousGerarcic)
    {

    }

    protected override void OnSelect()
    {
        if (puzzleCtrl != null)
            puzzleCtrl.CheckSolution();
    }
}
