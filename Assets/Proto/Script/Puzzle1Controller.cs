using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Controller : MonoBehaviour {

    public List<ButtonTagController> TaggedButtons = new List<ButtonTagController>();
    public List<SliderController> Sliders = new List<SliderController>();

    public Vector4 EVales = new Vector4(30, 70, 20, 90);

    private void Start()
    {
        foreach (ButtonTagController button in GetComponentsInChildren<ButtonTagController>())
        {
            TaggedButtons.Add(button);
            button.puzzleCtrl = this;
        }
    }

    public void SetEValues (int _E1, int _E2, int _E3, int _E4)
    {
        EVales.x += _E1;
        EVales.y += _E2;
        EVales.z += _E3;
        EVales.w += _E4;

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

    void DoWinningThings() { }
    void DoBreakThings() { }

    void UpdateSliderValues()
    {
        for (int i = 0; i < Sliders.Count; i++)
        {
            Sliders[i].SetFillAmount(EVales[i]);
        }
    }
}
