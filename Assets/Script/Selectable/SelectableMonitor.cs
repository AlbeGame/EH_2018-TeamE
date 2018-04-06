﻿using UnityEngine;

[RequireComponent(typeof(SelectableBehaviour))]
public class SelectableMonitor : MonoBehaviour, IPuzzleInput
{
    SelectableBehaviour selectable;
    IPuzzle puzzleCtrl;
    TextMesh textMesh;

    #region Data injection
    public IPuzzleInputData InputData;

    public void DataInjection(IPuzzleInputData _data)
    {
        InputData = _data;
    }
    #endregion

    public void Init(IPuzzle _parentPuzzle, IPuzzleInputData _data)
    {
        textMesh = GetComponentInChildren<TextMesh>();
        if (!textMesh)
            Debug.LogWarning("This component needs a TextMesh in order to work properly!");

        //Initial data injection
        InputData = _data;

        //setup parent relationship
        puzzleCtrl = _parentPuzzle;

        //selectable behaviour setup
        selectable = GetComponent<SelectableBehaviour>();
        selectable.Init((puzzleCtrl as MonoBehaviour).GetComponent<SelectableBehaviour>());
    }

    void Update ()
    {
        if (puzzleCtrl != null)
            puzzleCtrl.OnUpdateSelectable(this);
	}

    public void OnSelection()
    {
        if (puzzleCtrl != null)
            puzzleCtrl.OnMonitorSelect(this);
    }

    public void OnStateChange(SelectionState _newState) { }

    public void TypeOn(string _thingsToWrite, bool replaceOldText = true)
    {
        if(replaceOldText)
            textMesh.text = _thingsToWrite;
        else
        {
            textMesh.text = textMesh.text + _thingsToWrite;
        }
    }

    public string GetText()
    {
        return textMesh.text;
    }
}