using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : Button
{
    public Material SelectedMat;
    public Material UnselectedMat;
    MeshRenderer meshRenderer;

    public void Interact()
    {
        onClick.Invoke();
    }

    public void HighLight()
    {
        DoStateTransition(SelectionState.Highlighted, true);
    }

    public void NormalState()
    {
        DoStateTransition(SelectionState.Normal, true);
    }

    protected override void Awake()
    {
        base.Awake();
        transition = Transition.None;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnMouseUpAsButton()
    {
        onClick.Invoke();   
    }

    protected override void Start()
    {
        base.Start();
        NormalState();
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        if (currentSelectionState == state)
            return;

        if(state == SelectionState.Highlighted)
            meshRenderer.material.Lerp(meshRenderer.material, UnselectedMat, 0);
        else if(state != SelectionState.Disabled)
            meshRenderer.material.Lerp(meshRenderer.material, SelectedMat, 0);

    }

    void OnApplicationFocus(bool focus)
    {
        NormalState();
    }
}