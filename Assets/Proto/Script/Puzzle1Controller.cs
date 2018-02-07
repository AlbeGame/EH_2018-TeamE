using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Controller : MonoBehaviour
{
    public Vector4 EVales = new Vector4(30, 70, 20, 90);

    public Material BrokenMat;
    public Material SolvedMat;
    public PuzzleContainer container;
    public ButtonReset ResetButton;
    public List<ButtonTagController> TaggedButtons = new List<ButtonTagController>();
    public List<SliderController> Sliders = new List<SliderController>();


    private void Start()
    {
        ResetButton = GetComponentInChildren<ButtonReset>();
        ResetButton.puzzleCtrl = this;

        foreach (ButtonTagController button in GetComponentsInChildren<ButtonTagController>())
        {
            TaggedButtons.Add(button);
            button.puzzleCtrl = this;
        }
        foreach (SliderController slider in GetComponentsInChildren<SliderController>())
        {
            Sliders.Add(slider);
        }
        UpdateSliderValues();
    }

    public void SetEValues(Vector4 _eValues)
    {
        EVales += _eValues;
        for (int i = 0; i < 4; i++)
        {
            if (EVales[i] < 0)
                EVales[i] = 0;
            if (EVales[i] > 100)
                EVales[i] = 100;
        }

        UpdateSliderValues();
        CheckBreackDown();
    }

    public void CheckSolution()
    {
        for (int i = 0; i < 4; i++)
        {
            if (EVales[i] >= 70 && EVales[i] <= 80)
                continue;

            DoBreakThings();
            return;
        }

        DoWinningThings();
    }

    void CheckBreackDown()
    {
        for (int i = 0; i < 4; i++)
        {
            if (EVales[i] <= 0 && EVales[i] >= 100)
                DoBreakThings();
        }
    }

    void DoWinningThings()
    {
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            if(!renderer.GetComponent<TextMesh>())
                renderer.material = SolvedMat;
        }

        foreach (var item in TaggedButtons)
        {
            item.GetComponent<MeshCollider>().enabled = false;
        }
        ResetButton.GetComponent<MeshCollider>().enabled = false;

        container.Parent.Select();
    }
    void DoBreakThings()
    {
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            if(!renderer.GetComponent<TextMesh>())
                renderer.material = BrokenMat;
        }
    }

    void UpdateSliderValues()
    {
        for (int i = 0; i < Sliders.Count; i++)
        {
            Sliders[i].SetFillAmount(EVales[i]);
        }
    }
}
