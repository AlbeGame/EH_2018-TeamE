using UnityEngine;

/// <summary>
/// Class that acts as a switch (true/false) input device for puzzles
/// </summary>
[RequireComponent(typeof(SelectableBehaviour))]
public class SelectableSwitch : MonoBehaviour, IPuzzleInput
{
    [Tooltip("Keep it empty to apply on this GameObject")]
    bool _status;
    public bool selectStatus
    {
        get { return _status; }
        set
        {
            _status = value;
        }
    }
    SelectableBehaviour selectable;
    IPuzzle puzzleCtrl;

    #region IPuzzleInput
    public void Init(IPuzzle _parentPuzzle, IPuzzleInputData _data)
    {
        animCtrl = GetComponent<Animator>();

        //Initial data injection
        InputData = _data;

        //setup parent relationship
        puzzleCtrl = _parentPuzzle;

        //selectable behaviour setup
        selectable = GetComponent<SelectableBehaviour>();
        selectable.Init((puzzleCtrl as MonoBehaviour).GetComponent<SelectableBehaviour>());
        
        //starting swtich condition
        selectStatus = false;
    }

    public void OnSelection()
    {
        selectStatus = !selectStatus;
        puzzleCtrl.OnSwitchSelect(this);
    }

    public void OnStateChange(SelectionState _newState){}

    #region Animation
    Animator animCtrl;
    bool _isPressed;
    bool isPressed {
        get { return _isPressed; }
        set
        {
            if (_isPressed == value)
                return;
            else
            {
                _isPressed = value;
                animCtrl.SetBool("IsPressed", _isPressed);
            }
        }
    }
    private void OnMouseDown()
    {
        isPressed = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
    }
    #endregion

    #region Data injection
    public IPuzzleInputData InputData;
    /// <summary>
    /// Use it to modify held data on fly
    /// </summary>
    /// <param name="_data"></param>
    public void DataInjection(IPuzzleInputData _data)
    {
        InputData = _data;
    }
    #endregion
    #endregion
}
