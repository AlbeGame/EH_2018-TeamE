using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SelectableBehaviour))]
public class SelectionRoot : MonoBehaviour, ISelectable
{
    SelectableBehaviour selectable;

    CameraController camCtrl;

    public LevelSettings Setting;
    int PuzzleNeededToWin { get { return Setting.PuzzlesNeededToWin; } }
    public Altimetro Altimetro;
    public SelectableBehaviour AlarmPuzzle;
    PuzzleALARM_Data Alarm_Data { get { return Setting.Alarm_Data; } }

    List<ScriptableObject> PuzzleDatas { get { return Setting.PuzzleDatas; }}
    List<IPuzzle> puzzles = new List<IPuzzle>();
    public List<Transform> PuzzlePositions = new List<Transform>();

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        selectable = GetComponent<SelectableBehaviour>();
        selectable.Init(null, SelectionState.Selected);

        if (AlarmPuzzle)
        {
            AlarmPuzzle.Init(selectable);
            AlarmPuzzle.GetComponent<PuzzleALARM>().Setup(Alarm_Data);
            AlarmPuzzle.GetComponent<PuzzleALARM>().Init();
        }

        camCtrl = Camera.main.GetComponent<CameraController>();
        camCtrl.isMoveFreeCam = false;

        if (Altimetro)
            Altimetro.GetComponent<SelectableBehaviour>().Init(selectable);

        int randIndex;
        foreach (Transform puzzlePos in PuzzlePositions)
        {
            randIndex = Random.Range(0, PuzzleDatas.Count);
            IPuzzleData randData = PuzzleDatas[randIndex] as IPuzzleData;
            IPuzzle randPuzzle = Instantiate(randData.GetIPuzzleGO(), puzzlePos).GetComponent<IPuzzle>();
            (randPuzzle as MonoBehaviour).transform.SetParent(transform);
            randPuzzle.Setup(randData);
            randPuzzle.Init();
            puzzles.Add(randPuzzle);
            (randPuzzle as MonoBehaviour).GetComponent<SelectableBehaviour>().Init(selectable);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            SelectableBehaviour selected = selectable.GetChildren().FirstOrDefault(s => s.State == SelectionState.Selected);
            if (selected != null)
                selectable.Select();
            else if (selectable.State != SelectionState.Selected)
                selectable.Select();

            camCtrl.isMoveFreeCam = false;
        }

        if(selectable.State != SelectionState.Passive && Input.GetMouseButton(1))
            camCtrl.isMoveFreeCam = true;
    }

    bool hasAlarmedOnce;
    bool hasAlarmedAltitude;
    public void ActivateAlarm(bool forceIt = false)
    {
        PuzzleALARM _alarm = AlarmPuzzle.GetComponent<PuzzleALARM>();

        if (forceIt)
            _alarm.Toggle();
        else
        {
            ActivateAlarm();
        }

        hasAlarmedOnce = true;
    }

    public void ActivateAlarm()
    {
        PuzzleALARM _alarm = AlarmPuzzle.GetComponent<PuzzleALARM>();
        if (hasAlarmedOnce)
        {
            int actualChance = Random.Range(0, 100);
            if (actualChance >= 50)
                _alarm.Toggle();
        }
        else
            _alarm.Toggle();

        hasAlarmedOnce = true;
    }

    public void AccelerateAltimeter()
    {
        Altimetro.Accelerate();
    }

    public void DecelerateAltimeter(bool goPositive = false)
    {
        Altimetro.Decelerate(goPositive);
    }

    public void NotifyAltitudeUpdate(float _maxAltitude, float _currentAltitude)
    {
        NotifyAltitudeUpdate(_currentAltitude / _maxAltitude);
    }

    public void NotifyAltitudeUpdate(float percentage)
    {
        if (percentage >= 0.5f)
            return;

        if(percentage <= 0)
        { //gameOver
        }

        if (hasAlarmedAltitude)
            return;
        else
        {
            if (!hasAlarmedOnce)
                ActivateAlarm(true);
            hasAlarmedAltitude = true;
        }
    }

    public void NotifyPuzzleSolved(IPuzzle puzzle)
    {
        //Parziale comportamento comunque da refactorizzare
        selectable.Select();
        puzzle.SolutionState = PuzzleState.Solved;

        DecelerateAltimeter(puzzle.GetType() == typeof(PuzzleALARM)? true:false);
        UpdateOverallSolution();
    }

    public void NotifyPuzzleBreakdown(IPuzzle _puzzle)
    {
        //chiamata all'altimetro;
        _puzzle.SolutionState = PuzzleState.Broken;

        AccelerateAltimeter();
        AlarmPuzzle.GetComponent<PuzzleALARM>().Toggle(true);
    }

    public void OnSelection()
    {
        camCtrl.isMoveFreeCam = false;
        camCtrl.FocusReset();
    }

    public void OnStateChange(SelectionState _newState) { }

    void UpdateOverallSolution()
    {
        int currentSolvedPuzzles = 0;

        foreach (IPuzzle puzzle in puzzles)
        {
            if (puzzle.SolutionState == PuzzleState.Solved)
                currentSolvedPuzzles++;
        }

        //Momentanea Soluzione di vittoria
        if (currentSolvedPuzzles >= PuzzleNeededToWin)
            FindObjectOfType<MenuPauseController>().GoMainMenu(); 
    }
}
