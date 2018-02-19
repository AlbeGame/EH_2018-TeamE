using UnityEngine;
using UnityEngine.UI;

public class PuzzleTurbineButtonTagged : ISelectableBehaviour {

    PuzzleTurbine puzzleOwner;

    GameObject ButtonGraphic;
    string label;
    int[] valueModifier = new int[4];

    public PuzzleTurbineButtonTagged(PuzzleTurbine _puzzleOwner, TurbineButtonData _specifiData)
    {
        puzzleOwner = _puzzleOwner;
        valueModifier = _specifiData.EModifiers;
        label = _specifiData.Label;
    }

    public void OnSelect()
    {
        puzzleOwner.SetEValues(valueModifier[0], valueModifier[1], valueModifier[2], valueModifier[3]);

        puzzleOwner.Select(true);
    }

    public void OnInit(SelectableAbstract _older)
    {
        ButtonGraphic = _older.gameObject;
        ButtonGraphic.GetComponentInChildren<TextMesh>().text = label;
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
