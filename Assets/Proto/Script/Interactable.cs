using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : Button
{
    public Material SelectedMat;
    public Material UnselectedMat;
    public Material InteractedMat;
    public Material DisabledMat;
    MeshRenderer meshRenderer;

    #region API
    /// <summary>
    /// Called to trigger the "OnClick" event
    /// </summary>
    public void DoInteract()
    {
        onClick.Invoke();
    }
    /// <summary>
    /// Called to switch state to Highlghted
    /// </summary>
    public void DoHighLight()
    {
        DoStateTransition(SelectionState.Highlighted, true);
    }
    /// <summary>
    /// Called to switch state to Normal
    /// </summary>
    public void DoNormalState()
    {
        DoStateTransition(SelectionState.Normal, true);
    }
    /// <summary>
    /// Called to switch state to Disabled
    /// </summary>
    public void DoDisable()
    {
        DoStateTransition(SelectionState.Disabled, true);
    }
    #endregion

    #region Event hooking
    private void OnMouseUpAsButton()
    {
        onClick.Invoke();
    }

    private void OnMouseEnter()
    {
        if(currentSelectionState == SelectionState.Normal)
            DoHighLight();
    }

    private void OnMouseExit()
    {
        if (currentSelectionState == SelectionState.Highlighted)
            DoNormalState();
    }
    #endregion

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        if (currentSelectionState == state)
            return;
        if (meshRenderer)
        {
            switch (state)
            {
                case SelectionState.Normal:
                    meshRenderer.material.Lerp(meshRenderer.material, UnselectedMat, 1);
                    break;
                case SelectionState.Highlighted:
                    meshRenderer.material.Lerp(meshRenderer.material, SelectedMat, 1);
                    break;
                case SelectionState.Pressed:
                    meshRenderer.material.Lerp(meshRenderer.material, InteractedMat, 1);
                    break;
                case SelectionState.Disabled:
                    meshRenderer.material.Lerp(meshRenderer.material, DisabledMat, 1);
                    break;
            }
        }
    }

    #region Unity Life Flow
    protected override void Awake()
    {
        base.Awake();
        transition = Transition.None;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        DoNormalState();
    }

    void OnApplicationFocus(bool focus)
    {
        DoNormalState();
    }
    #endregion
}