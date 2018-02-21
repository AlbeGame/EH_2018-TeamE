public class PuzzleAutopilotButton : ISelectableBehaviour {

    public PuzzleAutopilot.InputValue Actualvalue;
    PuzzleAutopilot puzzleCtrl;

    public PuzzleAutopilotButton(PuzzleAutopilot.InputValue _data) {
        Actualvalue = _data;
    }

    public void OnInit(SelectableAbstract _holder) {
        puzzleCtrl = _holder as PuzzleAutopilot;
    }

    public void OnSelect() {
        puzzleCtrl.SetInput(Actualvalue);
    }
}

