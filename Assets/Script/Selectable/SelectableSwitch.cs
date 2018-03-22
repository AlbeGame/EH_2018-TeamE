using DG.Tweening;
using UnityEngine;

public class SelectableSwitch : SelectableAbstract
{
    public float BendDuration;
    public float BendAngle = 45;
    [Tooltip("Keep it empty to apply on this GameObject")]
    public GameObject ObjectToMove;
    bool _status;
    public bool selectStatus
    {
        get { return _status; }
        set
        {
            _status = value;
            Bend(_status ? 1 : -1);
        }
    }

    private IPuzzle puzzleCtrl;

    protected override void OnInitEnd(SelectableAbstract _parent)
    {
        //if (!ObjectToMove)
        //    originalRotation = transform.rotation;
        //else
        //    originalRotation = ObjectToMove.transform.rotation;

        selectStatus = false;
        puzzleCtrl = _parent as IPuzzle;
    }

    #region Data injection
    public IPuzzleInputData InputData;

    public void DataInjection(IPuzzleInputData _data)
    {
        InputData = _data;
    }
    #endregion

    protected override void OnSelect()
    {
        selectStatus = !selectStatus;
        puzzleCtrl.OnSwitchSelect(this);
    }

    void Bend(int _direction)
    {
        if (!ObjectToMove)
            transform.DOLocalRotate(Vector3.forward* _direction * BendAngle, BendDuration);
        else
            ObjectToMove.transform.DOLocalRotate(Vector3.forward * _direction * BendAngle, BendDuration);
    }
}
