using UnityEngine;

public class PuzzleGPS : SelectableItem, IPuzzle
{
    public PuzzleGPSData Data;
    public GPS_IO Interactables;

    Vector2Int solutionCoordinates;
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
        }
    }

    public void Setup(IPuzzleData _data)
    {
        Data = _data as PuzzleGPSData;
    }

    public void OnButtonSelect(SelectableButton _button) { }
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

        this.Select(true);
    }
    public void OnUpdateSelectable(SelectableAbstract _selectable) { }
    #endregion

    public void Init()
    {
        GenerateRandomCombination();
        InitOutputMonitor();
        InitNumerics();
        InitSelectableMonitors();
    }

    void InitNumerics()
    {
        for (int i = 0; i < Interactables.NumericalButtons.Length; i++)
        {
            Interactables.NumericalButtons[i].DataInjection(new PuzzleGPSNumericData() { ActualValue = i });
            Interactables.NumericalButtons[i].Init(this);
        }
    }

    void InitSelectableMonitors()
    {
        Interactables.Latitude.Init(this);
        Interactables.Latitude.DataInjection(new PuzzleGPSMonitorData());
        Interactables.Longitude.Init(this);
        Interactables.Latitude.DataInjection(new PuzzleGPSMonitorData());

        currentSelectedMonitor = Interactables.Latitude;
    }

    void InitOutputMonitor()
    {
        Interactables.OutputMonitor.Init(Data.GridDimension);
        Interactables.OutputMonitor.DisplayAndRotate(solutionCoordinates, solutionOrientation);
    }

    void GenerateRandomCombination()
    {
        int randCoordIndex = Random.Range(0, Data.PossibleCoordinates.Count);
        solutionCoordinates = Data.PossibleCoordinates[randCoordIndex];

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
