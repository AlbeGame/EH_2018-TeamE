using UnityEngine;

public class PuzzleGPS : SelectableItem, IPuzzle
{
    PuzzleGPSData data;
    public GPS_IO Interactables;

    PuzzleGPSData.PossibleCoordinate solutionCoordinates;
    float solutionOrientation;

    SelectableMonitor currentSelectedMonitor;
    
   
    #region IPuzzle
    PuzzleState _solutionState = PuzzleState.Unsolved;
    public PuzzleState SolutionState
    {
        get
        {
            return _solutionState;
        }

        set
        {
            _solutionState = value;
            OnSolutionStateChange(_solutionState);
        }
    }

    public void Setup(IPuzzleData _data)
    {
        data = _data as PuzzleGPSData;
        GenerateRandomCombination();
    }

    public bool CheckIfSolved()
    {
        int latitude = (Interactables.Latitude.InputData as PuzzleGPSMonitorData).coordinateValue;
        int longitude = (Interactables.Longitude.InputData as PuzzleGPSMonitorData).coordinateValue;

        if (solutionCoordinates.Coordinate.x == longitude && solutionCoordinates.Coordinate.y == latitude)
        {
            DoWin();
            return true;
        }
        else
        {
            DoLoose();
            return false;
        }
    }

    public void DoWin()
    {
        (GetRoot() as SelectionRoot).NotifyPuzzleSolved(this);
        graphicCtrl.Paint(_solutionState);
    }

    public void DoLoose()
    {
        (GetRoot() as SelectionRoot).NotifyPuzzleBreakdown(this);
        graphicCtrl.Paint(_solutionState);
       
    }

    public void OnButtonSelect(SelectableButton _button)
    {
        PuzzleGPSNumericData data = _button.InputData as PuzzleGPSNumericData;
        PuzzleGPSMonitorData coordinateData = currentSelectedMonitor.InputData as PuzzleGPSMonitorData;

        Select(true);
        if (data.ActualValue < 10)
        {
            coordinateData.coordinateValue *= 10;
            coordinateData.coordinateValue += data.ActualValue;
            currentSelectedMonitor.TypeOn(coordinateData.coordinateValue.ToString());
        }
        else if(data.ActualValue == 10)
        {
            coordinateData.coordinateValue /= 10;
            currentSelectedMonitor.TypeOn(coordinateData.coordinateValue.ToString());
        }
        else if(data.ActualValue == 11)
        {
            CheckIfSolved();
        }
    }
    public void OnSwitchSelect(SelectableSwitch _switch) { }
    public void OnMonitorSelect(SelectableMonitor _monitor) {
        if(_monitor == Interactables.Latitude)
        {
            currentSelectedMonitor = Interactables.Latitude;
        }
        else if (_monitor == Interactables.Longitude)
        {
            currentSelectedMonitor = Interactables.Longitude;
        }

        Select(true);
    }

    public void OnUpdateSelectable(SelectableAbstract _selectable)
    {
        //CurrentSelectedMonitor deve lampeggiare.
    }
    #endregion

    #region Selectable Behaviours
    protected override void OnInitEnd(SelectableAbstract _parent)
    {
        InitOutputMonitor();
        InitNumerics();
        InitSelectableMonitors();
    }

    protected override void OnStateChange(SelectionState _state)
    {
        if(SolutionState == PuzzleState.Unsolved)
            base.OnStateChange(_state);
    }

    protected override void OnSelect()
    {
        base.OnSelect();

        Debugger.DebugLogger.Clean();
        Debugger.DebugLogger.LogText("------------//" + gameObject.name + "//-----------");
        Debugger.DebugLogger.LogText(gameObject.name + gameObject.GetInstanceID());
        Debugger.DebugLogger.LogText(solutionCoordinates.Coordinate.ToString());
    }
    #endregion

    void OnSolutionStateChange(PuzzleState _solutionState)
    {
        graphicCtrl.Paint(_solutionState);
    }

    void InitNumerics()
    {
        for (int i = 0; i < Interactables.NumericalButtons.Length; i++)
        {
            Interactables.NumericalButtons[i].Init(this);
            Interactables.NumericalButtons[i].DataInjection(new PuzzleGPSNumericData() { ActualValue = i });
        }
    }

    void InitSelectableMonitors()
    {
        Interactables.Latitude.Init(this);
        Interactables.Latitude.DataInjection(new PuzzleGPSMonitorData());
        Interactables.Longitude.Init(this);
        Interactables.Longitude.DataInjection(new PuzzleGPSMonitorData());

        ////DEBUG POURPOSE ONLY
        //Interactables.Latitude.TypeOn(solutionCoordinates.x.ToString());
        //Interactables.Longitude.TypeOn(solutionCoordinates.y.ToString());
        ////---------

        currentSelectedMonitor = Interactables.Latitude;
    }

    void InitOutputMonitor()
    {
        Interactables.OutputMonitor.Init(data);
        Interactables.OutputMonitor.DisplayAndRotate(solutionCoordinates, solutionOrientation);
    }

    void GenerateRandomCombination()
    {
        int randCoordIndex = Random.Range(0, data.PossibleCoordinates.Count);
        solutionCoordinates = data.PossibleCoordinates[randCoordIndex];

        int randOrient = Random.Range(0, 4);
        solutionOrientation = randOrient * 90;
    }

    [System.Serializable]
    public class GPS_IO
    {
        public SelectableButton[] NumericalButtons = new SelectableButton[12];
        public SelectableMonitor Latitude;
        public SelectableMonitor Longitude;
        public PuzzleGPSOutputMonitor OutputMonitor;
    }
}
