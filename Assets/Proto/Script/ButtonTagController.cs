using UnityEngine;

public class ButtonTagController : SelectableItem {

    public Puzzle1Controller puzzleCtrl;
    public TextMesh Label;
    public GameObject ButtonGraphic;
    public float PushOffSet = .002f;
    Vector3 originalPos;
    public Vector4 ValuesModifier = new Vector4();

    protected override void OnSelect()
    {
        if (puzzleCtrl != null)
            puzzleCtrl.SetEValues(ValuesModifier);
    }

    protected override void OnInit(SelectionManager _selectMng)
    {
        originalPos = transform.position;
        if(Label != null)
            Label = GetComponentInChildren<TextMesh>();
    }

    private void OnMouseDown()
    {
        transform.position = new Vector3(originalPos.x, originalPos.y - PushOffSet, originalPos.z);
    }

    private void OnMouseUp()
    {
        transform.position = originalPos;
    }

    public void OverrideLabel(string _text)
    {
        if(Label != null)
            Label.text = _text;
    }
}
