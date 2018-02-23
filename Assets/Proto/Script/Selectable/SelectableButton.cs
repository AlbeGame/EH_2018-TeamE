using UnityEngine;
using DG.Tweening;

public class SelectableButton : SelectableAbstract
{
    //public float PushDuration = .5f;
    //public float PushOffSet = .005f;
    //[Tooltip("Keep it empty to apply on this GameObject")]
    //public GameObject ObjectToMove;
    public TextMesh Text;
    public Renderer IconRenderer;
    //Vector3 originalPos;

    private IPuzzle puzzleController;

    protected override void OnInitEnd(SelectableAbstract _parent) {
        //if (!ObjectToMove)
        //    originalPos = transform.position;
        //else
        //    originalPos = ObjectToMove.transform.position;

        puzzleController = _parent as IPuzzle;
    }

    public void SetAdditionalData(string _label = "", Material _iconMat = null) {
        //Label Set
        if (Text != null)
            Text.text = _label;

        //Icon Set
        if (IconRenderer != null)
            IconRenderer.material = _iconMat;
    }

    #region Data injection
    public IPuzzleInputData InputData;

    public void DataInjection(IPuzzleInputData _data) {
        InputData = _data;
    }
    #endregion

    protected override void OnSelect() {
        //specificBehaviour.OnSelect();
        puzzleController.OnButtonSelect(this);
        Parent.Select(true);
    }

    //private void OnMouseUp() {
    //    if (!ObjectToMove)
    //        transform.DOMove(originalPos, PushDuration / 2);
    //    else
    //        ObjectToMove.transform.DOMove(originalPos, PushDuration / 2);
    //}

    //private void OnMouseDown() {
    //    if (!ObjectToMove)
    //        transform.DOMoveY(originalPos.y - PushOffSet, PushDuration / 2);
    //    else
    //        ObjectToMove.transform.DOMoveY(originalPos.y - PushOffSet, PushDuration / 2);
    //}
}
