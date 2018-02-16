using UnityEngine;

public class PuzzleTurbineButtonTagged : ISelectableBehaviour {

    PuzzleTurbine puzzleOwner;

    GameObject ButtonGraphic;
    float PushOffSet = .002f;
    Vector3 originalPos;
    int[] valueModifier = new int[4];
    public Vector4 ValuesModifier = new Vector4();

    public PuzzleTurbineButtonTagged(PuzzleTurbine _puzzleOwner)
    {
        puzzleOwner = _puzzleOwner;
    }

    public void OnSelect()
    {
        puzzleOwner.SetEValues(valueModifier[0], valueModifier[1], valueModifier[2], valueModifier[3]);

        puzzleOwner.Select(true);
    }

    public void OnInit(SelectableItem _older)
    {
        ButtonGraphic = _older.gameObject;
        originalPos = ButtonGraphic.transform.position;
    }

    public void OnMouseDown()
    {
        ButtonGraphic.transform.position = new Vector3(originalPos.x, originalPos.y - PushOffSet, originalPos.z);
    }

    public void OnMouseUp()
    {
        ButtonGraphic.transform.position = originalPos;
    }
}
