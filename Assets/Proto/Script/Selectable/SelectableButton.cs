using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableButton : SelectableAbstract
{
    public ISelectableBehaviour specificBehaviour;
    public PuzzleType Puzzle;
    public ButtonType Type;

    float PushOffSet = .002f;
    Vector3 originalPos;

    protected override void OnInitEnd(SelectableAbstract _parent)
    {
        originalPos = transform.position;
        specificBehaviour.OnInit(this);
    }

    protected override void OnSelect()
    {
        specificBehaviour.OnSelect();
    }

    private void OnMouseUp()
    {
        transform.position = originalPos;
    }

    private void OnMouseDown()
    {
        transform.position = new Vector3(originalPos.x, originalPos.y - PushOffSet, originalPos.z);
    }
}

public enum ButtonType
{
    Untagged,
    Tagged
}
