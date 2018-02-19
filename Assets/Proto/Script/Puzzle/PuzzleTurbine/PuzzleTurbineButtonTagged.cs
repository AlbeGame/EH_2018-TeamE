using UnityEngine;

public class PuzzleTurbineButtonTagged : ISelectableBehaviour {

    PuzzleTurbine puzzleOwner;

    GameObject ButtonGraphic;
    float PushOffSet = .002f;
    Vector3 originalPos;
    int[] valueModifier = new int[4];

    public PuzzleTurbineButtonTagged(PuzzleTurbine _puzzleOwner, TurbineButtonData _specifiData)
    {
        puzzleOwner = _puzzleOwner;
        valueModifier = _specifiData.EModifiers;
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

[System.Serializable]
public class TurbineButtonData
{
    public string Label;
    public int[] EModifiers { get { return new int[] { E1Modifier, E2Modifier, E3Modifier, E4Modifier }; } }
    public int E1Modifier;
    public int E2Modifier;
    public int E3Modifier;
    public int E4Modifier;
}
