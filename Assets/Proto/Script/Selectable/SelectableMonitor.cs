using UnityEngine;

public class SelectableMonitor : SelectableAbstract {

    IPuzzle puzzleCtrl;
    TextMesh textMesh; 

    protected override void OnInitEnd(SelectableAbstract _parent)
    {
        textMesh = GetComponentInChildren<TextMesh>();

        if(_parent.GetType() == typeof(IPuzzle))
            puzzleCtrl = _parent as IPuzzle;
    }

    void Update () {

        puzzleCtrl.OnUpdateSelectable(this);
	}

    protected override void OnSelect()
    {
        if (puzzleCtrl != null)
            puzzleCtrl.OnMonitorSelect(this);
    }
}
