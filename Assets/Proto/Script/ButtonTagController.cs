using UnityEngine;

public class ButtonTagController : SelectableItem {

    public Puzzle1Controller puzzleCtrl;
    public TextMesh Label;
    public GameObject ButtonGraphic;
    public float PushOffSet = .002f;
    Vector3 originalPos;
    public Vector4 ValuesModifier = new Vector4();

    private void Start()
    {
        Init(null);
    }

    protected override void OnSelect()
    {
        if (puzzleCtrl != null)
            puzzleCtrl.SetEValues(ValuesModifier);

        Parent.Select();
    }

    protected override void OnInitEnd(SelectableItem _parent)
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
