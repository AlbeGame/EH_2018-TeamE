using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectableBehaviour), typeof(PuzzleGraphic))]
public class PuzzlePressure : MonoBehaviour, IPuzzle, ISelectable
{
    SelectableBehaviour selectable;
    PuzzleGraphic graphicCtrl;

    PuzzlePressureData data;
    public Pressure_IO Interactables;

    public PuzzleState SolutionState
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
            throw new System.NotImplementedException();
        }
    }

    public bool CheckIfSolved()
    {
        throw new System.NotImplementedException();
    }

    public void DoLoose()
    {
        throw new System.NotImplementedException();
    }

    public void DoWin()
    {
        throw new System.NotImplementedException();
    }

    public void Init()
    {
        throw new System.NotImplementedException();
    }

    public void OnButtonSelect(SelectableButton _button)
    {
        throw new System.NotImplementedException();
    }

    public void OnMonitorSelect(SelectableMonitor _monitor)
    {
        throw new System.NotImplementedException();
    }

    public void OnSelection()
    {
        throw new System.NotImplementedException();
    }

    public void OnStateChange(SelectionState _state)
    {
        throw new System.NotImplementedException();
    }

    public void OnSwitchSelect(SelectableSwitch _switch)
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdateSelectable(IPuzzleInput _input)
    {
        throw new System.NotImplementedException();
    }

    public void Setup(IPuzzleData data)
    {
        throw new System.NotImplementedException();
    }

    [System.Serializable]
    public class Pressure_IO
    {
        public SelectableButton[] NumericalButtons = new SelectableButton[3];
        public SliderController Slider;
        public TextMesh ErrorText;
        //Aggiungere monitor di output
    }

    public enum ButtonType
    {
        Red = 0,
        Blue = 1,
        Green = 2
    }
}
