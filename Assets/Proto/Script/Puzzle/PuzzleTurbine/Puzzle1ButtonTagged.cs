using UnityEngine;

public class Puzzle1ButtonTagged : SelectableItem {

    public PuzzleTurbine puzzleCtrl;
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

        Parent.Select(true);
    }

    protected override void OnInitEnd(SelectableItem _parent)
    {
        originalPos = transform.position;
    }

    private void OnMouseDown()
    {
        transform.position = new Vector3(originalPos.x, originalPos.y - PushOffSet, originalPos.z);
    }

    private void OnMouseUp()
    {
        transform.position = originalPos;
    }
}
