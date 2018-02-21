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
    bool _status;
    bool status
    {
        get { return _status; }
        set
        {
            _status = value;
            Bend(_status ? 1 : -1);
        }
    }

    protected override void OnInitEnd(SelectableAbstract _parent)
    {
        if (!ObjectToMove)
            originalRotation = transform.rotation;
        else
            originalRotation = ObjectToMove.transform.rotation;

        specificBehaviour.OnInit(this);
        status = false;
    }

    protected override void OnSelect()
    {
        status = !status;
        specificBehaviour.OnSelect();
    }

    void Bend(int _direction)
    {
        if (!ObjectToMove)
            transform.DORotate(Vector3.forward * _direction * BendAngle, BendDuration);
        else
            ObjectToMove.transform.DORotate(Vector3.forward * _direction * BendAngle, BendDuration);
    }
}
