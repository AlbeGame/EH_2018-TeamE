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

    PuzzlePressureData.Setup currentSetup;

    public PuzzleState SolutionState { get; set; }

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
        Interactables.OutputMonitor.Toggle(true);
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

    public void Setup(IPuzzleData _data)
    {
        selectable = GetComponent<SelectableBehaviour>();
        graphicCtrl = GetComponent<PuzzleGraphic>();

        //Choosing setups between the possibilities
        data = _data as PuzzlePressureData;

        Interactables.OutputMonitor.Toggle(false);
        //Inserire setup IO
    }

    public void InitOutputMonitor()
    {
        int _setupIndex = Random.Range(0, data.Setups.Count);
        currentSetup = data.Setups[_setupIndex];

        Interactables.OutputMonitor.ImageToDisplay = currentSetup.ImgToDispaly;
    }

    [System.Serializable]
    public class Pressure_IO
    {
        public SelectableButton[] NumericalButtons = new SelectableButton[3];
        public SliderController Slider;
        public TextMesh ErrorText;
        public PuzzlePressureOutputMonitor OutputMonitor;
    }

    public enum ButtonType
    {
        Red = 0,
        Blue = 1,
        Green = 2
    }

    public class ButtonData : IPuzzleInputData
    {
        public ButtonType Type;
    }
}
