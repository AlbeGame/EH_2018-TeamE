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
        if(state == SelectionState.Highlighted)
            meshRenderer.material.Lerp(SelectedMat, UnselectedMat, 0);
    }

    void OnApplicationFocus(bool focus)
    {
        NormalState();
    }
}