using UnityEngine;

public class ButtonReset : SelectableItem {

    public Puzzle1Controller puzzleCtrl;
    public GameObject ButtonGraphic;

    private void Start()
    {
        Init(null);
    }

    protected override void OnSelect()
    {
        if (puzzleCtrl != null)
            puzzleCtrl.CheckSolution();
    }
}
