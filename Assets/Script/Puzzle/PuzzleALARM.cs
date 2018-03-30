using UnityEngine;

[RequireComponent(typeof(SelectableBehaviour), typeof(PuzzleGraphic))]
public class PuzzleALARM : MonoBehaviour, IPuzzle, ISelectable
{
    PuzzleALARM_Data data;

    #region IPuzzle
    PuzzleState _solutionState;
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

    public void Setup(IPuzzleData data)
    {
        throw new System.NotImplementedException();
    }

    public void Init()
    {
        throw new System.NotImplementedException();
    }
    public void DoWin()
    {
        throw new System.NotImplementedException();
    }
    public void DoLoose()
    {
        throw new System.NotImplementedException();
    }

    public void OnButtonSelect(SelectableButton _button)
    {
        throw new System.NotImplementedException();
    }
    public void OnSwitchSelect(SelectableSwitch _switch)
    {
        throw new System.NotImplementedException();
    }
    public void OnMonitorSelect(SelectableMonitor _monitor)
    {
        throw new System.NotImplementedException();
    }
    public void OnUpdateSelectable(IPuzzleInput _input)
    {
        throw new System.NotImplementedException();
    }

    public bool CheckIfSolved()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region ISelectable
    public void OnSelection()
    {
        throw new System.NotImplementedException();
    }
    public void OnStateChange(SelectionState _state)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    public enum InputValue
    {
        Button_1, Button_1A, Button_1B, Button_1C, Button_1D,
        Button_2, Button_2A, Button_2B, Button_2C, Button_2D,
        Button_3, Button_3A, Button_3B, Button_3C, Button_3D,
        Button_4, Button_4A, Button_4B, Button_4C, Button_4D,
        Button_5, Switch_5A, Switch_5B, Switch_5C, Switch_5D,
        Button_6, Switch_6A, Switch_6B, Switch_6C, Switch_6D,
        Button_7, Switch_7A, Switch_7B, Switch_7C, Switch_7D,
        Button_8, Switch_8A, Switch_8B, Switch_8C, Switch_8D
    }

    public struct ALARM_IO
    {
        SelectableButton Button_1, Button_1A, Button_1B, Button_1C, Button_1D,
                            Button_2, Button_2A, Button_2B, Button_2C, Button_2D,
                            Button_3, Button_3A, Button_3B, Button_3C, Button_3D,
                            Button_4, Button_4A, Button_4B, Button_4C, Button_4D,
                            Button_5, Button_6, Button_7, Button_8;

        SelectableSwitch Switch_5A, Switch_5B, Switch_5C, Switch_5D,
                            Switch_6A, Switch_6B, Switch_6C, Switch_6D,
                            Switch_7A, Switch_7B, Switch_7C, Switch_7D,
                            Switch_8A, Switch_8B, Switch_8C, Switch_8D;
    }                    
}                        
                         