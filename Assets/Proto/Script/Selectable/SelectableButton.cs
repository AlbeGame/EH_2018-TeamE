using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableButton : SelectableItem
{
    public ISelectableBehaviour specificBehaviour;
    public PuzzleType Puzzle;
    public ButtonType Type;

    protected override void OnInitEnd(SelectableItem _parent)
    {
        specificBehaviour.OnInit(this);
    }

    protected override void OnSelect()
    {
        specificBehaviour.OnSelect();
    }

    private void OnMouseUp()
    {
        specificBehaviour.OnMouseUp();
    }

    private void OnMouseDown()
    {
        specificBehaviour.OnMouseDown();
    }
}

public enum ButtonType
{
    Untagged,
    Tagged
}
