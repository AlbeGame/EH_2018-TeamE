using UnityEngine;

public class Puzzle1ButtonReset : SelectableItem {

    public PuzzleTurbine puzzleCtrl;
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
