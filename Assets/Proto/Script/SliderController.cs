using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour {

    public float SmallerValue = -0.085f;
    public float HigherValue = 0.085f;
    float maxLenght { get { return HigherValue - SmallerValue; } }

    public Material PositiveColor;
    public Material NeutralColor;
    public Material NegativeColor;

    LineRenderer renderer;

    private void Start()
    {
        renderer = GetComponentInChildren<LineRenderer>();
    }

    public void SetFillAmount(float _percentage)
    {
        Vector3 newLineHead = renderer.GetPosition(renderer.positionCount - 1);

        newLineHead.y = SmallerValue + maxLenght * _percentage / 100;

        renderer.SetPosition(renderer.positionCount - 1, newLineHead);

        AdaptMaterial(_percentage);
    }

    void AdaptMaterial(float _percentage)
    {
        if (_percentage > 80)
            renderer.material = NegativeColor;
        else if (_percentage < 70)
            renderer.material = NeutralColor;
        else
            renderer.material = PositiveColor;
    }
}
