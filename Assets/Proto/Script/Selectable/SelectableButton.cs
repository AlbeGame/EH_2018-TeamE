using UnityEngine;
using DG.Tweening;

public class SelectableButton : SelectableAbstract
{
    public TextMesh Text;
    public Renderer IconRenderer;

    private IPuzzle puzzleController;

    protected override void OnInitEnd(SelectableAbstract _parent)
    {
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

    protected override void OnSelect()
    {
        puzzleController.OnButtonSelect(this);
    }
}
