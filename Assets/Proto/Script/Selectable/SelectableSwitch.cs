using DG.Tweening;
using UnityEngine;

public class SelectableSwitch : SelectableAbstract
{
    public ISelectableBehaviour specificBehaviour;
    public PuzzleType Puzzle;

    public float BendDuration;
    public float BendAngle = 45;
    [Tooltip("Keep it empty to apply on this GameObject")]
    public GameObject ObjectToMove;
    Quaternion originalRotation;

    protected override void OnInitEnd(SelectableAbstract _parent)
    {
        if (!ObjectToMove)
            originalRotation = transform.rotation;
        else
            originalRotation = ObjectToMove.transform.rotation;

        specificBehaviour.OnInit(this);
    }

    protected override void OnSelect()
    {
        specificBehaviour.OnSelect();
    }

    void Bend()
    {
        if(!ObjectToMove)
            transform.DORotate(Vector3.forward * BendAngle, BendDuration);
        else
            ObjectToMove.transform.DORotate(Vector3.forward * BendAngle, BendDuration);
    }
}
