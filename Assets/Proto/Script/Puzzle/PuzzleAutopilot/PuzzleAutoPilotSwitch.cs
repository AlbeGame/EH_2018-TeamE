public class PuzzleAutoPilotSwitch : ISelectableBehaviour
{
    PuzzleAutopilot puzzleCtrl;
    bool _status;
    bool status
    {
        get { return _status; }
        set {
            _status = value;
            if (_status && (int)currentValue % 2 != 0)
                currentValue--;
            else if (!_status && (int)currentValue % 2 == 0)
                currentValue++;
        }
    }
    PuzzleAutopilot.InputValue _currentValue;
    PuzzleAutopilot.InputValue currentValue
    {
        get { return _currentValue; }
        set
        {
            _currentValue = value;
            status = (int)_currentValue % 2 == 0 ? true : false;
        }
    }

    public PuzzleAutoPilotSwitch(PuzzleAutopilot.InputValue _startingValue)
    {
        currentValue = _startingValue;
    }

    public void OnInit(SelectableAbstract _holder)
    {
        puzzleCtrl = _holder as PuzzleAutopilot;
    }

    public void OnSelect()
    {
        status = !status;
    }
}
