public class PuzzleGPSNumeric : ISelectableBehaviour
{
    PuzzleGPS puzzleCtrl;
    int actualValue;

    public PuzzleGPSNumeric(int _value)
    {
        actualValue = _value;
    }

    public void OnInit(SelectableAbstract _holder)
    {
        puzzleCtrl = _holder as PuzzleGPS;
    }

    public void OnSelect()
    {
        throw new System.NotImplementedException();
    }
}
