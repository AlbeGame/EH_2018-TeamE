using UnityEngine;

public class ButtonTagController : SelectableItem {

    public Puzzle1Controller puzzleCtrl;
    public TextMesh Label;
    public GameObject ButtonGraphic;

    public int E1Increment;
    public int E2Increment;
    public int E3Increment;
    public int E4Increment;

    protected override void OnSelect()
    {
        if (puzzleCtrl != null)
            puzzleCtrl.SetEValues(E1Increment,E2Increment,E3Increment,E4Increment);
    }

    protected override void OnInit(SelectionManager _selectMng)
    {
        Label = GetComponentInChildren<TextMesh>();
    }

    public void OverrideLabel(string _text)
    {
        Label.text = _text;
    }
}
