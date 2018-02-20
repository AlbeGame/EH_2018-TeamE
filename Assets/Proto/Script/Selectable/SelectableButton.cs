using UnityEngine;
using DG.Tweening;

public class SelectableButton : SelectableAbstract
{
    public ISelectableBehaviour specificBehaviour;
    public PuzzleType Puzzle;
    public ButtonType Type;

    public float PushDuration = .5f;
    public float PushOffSet = .005f;
    [Tooltip("Keep it empty to apply on this GameObject")]
    public GameObject ObjectToMove;
    Vector3 originalPos;

    protected override void OnInitEnd(SelectableAbstract _parent)
    {
        if (!ObjectToMove)
            originalPos = transform.position;
        else
            originalPos = ObjectToMove.transform.position;

        specificBehaviour.OnInit(this);
    }

    protected override void OnSelect()
    {
        specificBehaviour.OnSelect();
    }

    private void OnMouseUp()
    {
        if (!ObjectToMove)
            transform.DOMove(originalPos, PushDuration/2);
        else
            ObjectToMove.transform.DOMove(originalPos, PushDuration/2);
    }

    private void OnMouseDown()
    {
        if(!ObjectToMove)
            transform.DOMoveY(originalPos.y - PushOffSet, PushDuration/2);/* = new Vector3(originalPos.x, originalPos.y - PushOffSet, originalPos.z);*/
        else
            ObjectToMove.transform.DOMoveY(originalPos.y - PushOffSet, PushDuration / 2);
    }
}

public enum ButtonType
{
    Untagged,
    Tagged
}
