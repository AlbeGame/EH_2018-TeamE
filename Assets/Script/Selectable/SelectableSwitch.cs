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
            if(_status != value)
            {
                _status = value;
                if (_status)
                    isPressed = true;
                else
                    isPressed = false;
            }
        }
    }
    SelectableBehaviour selectable;
    IPuzzle puzzleCtrl;

    #region IPuzzleInput
    public void Init(IPuzzle _parentPuzzle, IPuzzleInputData _data)
    {
        AnimatorCtrl = GetComponent<Animator>();

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
        GameManager.I_GM.AudioManager.PlaySound(AudioType.InputClick);
        selectStatus = !selectStatus;
        puzzleCtrl.OnSwitchSelect(this);
    }

    public void OnStateChange(SelectionState _newState)
    {
        if(_newState == SelectionState.Highlighted)
            GameManager.I_GM.AudioManager.PlaySound(AudioType.InputHover);
    }

    #region Animation
    public Animator AnimatorCtrl;
    public string ToLeftAnim;
    public string ToRightAnim;
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
                if (AnimatorCtrl)
                {
                    if (_isPressed)
                        AnimatorCtrl.Play(ToRightAnim);
                    else
                        AnimatorCtrl.Play(ToLeftAnim);
                }
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
