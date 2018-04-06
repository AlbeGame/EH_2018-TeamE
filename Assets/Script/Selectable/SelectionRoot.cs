﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SelectableBehaviour))]
public class SelectionRoot : MonoBehaviour, ISelectable
{
    SelectableBehaviour selectable;

    CameraController camCtrl;
    public int PuzzleNeededToWin;
    int currentSolvedPuzzles;
    public Altimetro Altimetro;
    public SelectableBehaviour AlarmPuzzle;

    public List<ScriptableObject> PuzzleDatas = new List<ScriptableObject>();
    public List<Transform> PuzzlePositions = new List<Transform>();

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        selectable = GetComponent<SelectableBehaviour>();
        selectable.Init();

        if (AlarmPuzzle) { }
            AlarmPuzzle.Init(selectable);

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

            camCtrl.isMoveFreeCam = false;
        }

        if(selectable.State != SelectionState.Passive && Input.GetMouseButton(1))
            camCtrl.isMoveFreeCam = true;
    }

    public void NotifyPuzzleSolved(IPuzzle puzzle)
    {
        //Parziale comportamento comunque da refactorizzare
        selectable.Select();
        puzzle.SolutionState = PuzzleState.Solved;

        currentSolvedPuzzles++;
        if (currentSolvedPuzzles >= PuzzleNeededToWin)
            FindObjectOfType<MenuPauseController>().GoMainMenu(); //Momentanea Soluzione di vittoria
    }

    public void NotifyPuzzleBreakdown(IPuzzle _puzzle)
    {
        //chiamata all'altimetro;
        _puzzle.SolutionState = PuzzleState.Broken;
    }

    public void OnSelection()
    {
        camCtrl.isMoveFreeCam = false;
        camCtrl.FocusReset();
    }

    public void OnStateChange(SelectionState _newState) { }
}