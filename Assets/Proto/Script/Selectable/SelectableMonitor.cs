public class SelectableMonitor : SelectableAbstract {

    IPuzzle puzzleCtrl;
    public bool IsEditable { get; private set; }

    protected override void OnInitEnd(SelectableAbstract _parent)
    {
        if(_parent.GetType() == typeof(IPuzzle))
            puzzleCtrl = _parent as IPuzzle;
    }

    // Update is called once per frame
    void Update () {
		
	}

    protected override void OnSelect()
    {
        if (puzzleCtrl != null)
            puzzleCtrl.OnMonitorSelect(this);
    }
}
