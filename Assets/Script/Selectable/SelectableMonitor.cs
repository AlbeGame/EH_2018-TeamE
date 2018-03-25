using UnityEngine;

public class SelectableMonitor : SelectableAbstract {

    IPuzzle puzzleCtrl;
    TextMesh textMesh;

    #region Data injection
    public IPuzzleInputData InputData;

    public void DataInjection(IPuzzleInputData _data)
    {
        InputData = _data;
    }
    #endregion

    protected override void OnInitEnd(SelectableAbstract _parent)
    {
        textMesh = GetComponentInChildren<TextMesh>();

        puzzleCtrl = _parent as IPuzzle;
    }

    void Update ()
    {
        if (puzzleCtrl != null)
            puzzleCtrl.OnUpdateSelectable(this);
	}

    protected override void OnSelect()
    {
        if (puzzleCtrl != null)
            puzzleCtrl.OnMonitorSelect(this);
    }

    public void TypeOn(string _thingsToWrite, bool replaceOldText = true)
    {
        if(replaceOldText)
            textMesh.text = _thingsToWrite;
        else
        {
            textMesh.text = textMesh.text + _thingsToWrite;
        }
    }

    public string GetText()
    {
        return textMesh.text;
    }
}
