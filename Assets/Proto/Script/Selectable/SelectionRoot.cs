using System.Collections.Generic;
using UnityEngine;

public class SelectionRoot : SelectableAbstract
{
    CameraController camCtrl;
    public int PuzzleNeededToWin;
    int currentSolvedPuzzles;
    public Altimetro Altimetro;
    public List<ScriptableObject> PuzzleDatas = new List<ScriptableObject>();
    public List<Transform> PuzzlePositions = new List<Transform>();

    private void Start()
    {
        Init(false, SelectionState.Selected);
    }

    protected override void OnInitEnd(SelectableAbstract _parent)
    {
        camCtrl = Camera.main.GetComponent<CameraController>();
        camCtrl.isMoveFreeCam = false;

        if (Altimetro)
            Altimetro.GetComponent<SelectableItem>().Init(this);

        int randIndex;
        foreach (Transform puzzlePos in PuzzlePositions)
        {
            randIndex = Random.Range(0, PuzzleDatas.Count);
            IPuzzleData randData = PuzzleDatas[randIndex] as IPuzzleData;
            SelectableItem randPuzzle = Instantiate(randData.GetIPuzzleGO(), puzzlePos).GetComponent<SelectableItem>();
            (randPuzzle as IPuzzle).Setup(randData);
            randPuzzle.Init(this);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            if(hasASelectedChild)
                Select(true);

            camCtrl.isMoveFreeCam = false;
        }

        if(State == SelectionState.Selected && Input.GetMouseButton(1))
            camCtrl.isMoveFreeCam = true;
    }

    public void NotifyPuzzleSolved(IPuzzle puzzle)
    {
        //Parziale comportamento comunque da refactorizzare
        Select(true);
        puzzle.SolutionState = PuzzleState.Solved;
        (puzzle as SelectableItem).State = SelectionState.Unselectable;

        currentSolvedPuzzles++;
        if (currentSolvedPuzzles >= PuzzleNeededToWin)
            FindObjectOfType<MenuPauseController>().GoMainMenu(); //Momentanea Soluzione di vittoria
    }

    public void NotifyPuzzleBreakdown(IPuzzle _puzzle)
    {
        //chiamata all'altimetro;
        _puzzle.SolutionState = PuzzleState.Broken;
    }

    protected override void OnSelect()
    {
        camCtrl.isMoveFreeCam = false;
        camCtrl.FocusReset();
    }
}
