using UnityEngine;

public class ButtonTagController : SelectableItem {

    public Puzzle1Controller puzzleCtrl;
    public TextMesh Label;
    public GameObject ButtonGraphic;

    public Vector4 ValuesModifier = new Vector4();

    protected override void OnSelect()
    {
        if (puzzleCtrl != null)
            puzzleCtrl.SetEValues(ValuesModifier);
    }

    protected override void OnInit(SelectionManager _selectMng)
    {
        if(Label != null)
            Label = GetComponentInChildren<TextMesh>();
    }

    public void OverrideLabel(string _text)
    {
        if(Label != null)
            Label.text = _text;
    }
}
