using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour {

    public Transform LineBegin;
    public Transform LineEnd;
    Vector3 maxLenght { get { return LineEnd.position - LineBegin.position; } }

    public Material PositiveColor;
    public Material NeutralColor;
    public Material NegativeColor;

    LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.SetPosition(0, LineBegin.position);
        lineRenderer.SetPosition(1, LineEnd.position);
    }

    public void SetFillAmount(float _percentage)
    {
        Vector3 newLineHead;

        newLineHead = LineBegin.position + maxLenght * _percentage / 100;

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newLineHead);

        AdaptMaterial(_percentage);
    }

    void AdaptMaterial(float _percentage)
    {
        if (_percentage > 80)
            lineRenderer.material = NegativeColor;
        else if (_percentage < 70)
            lineRenderer.material = NeutralColor;
        else
            lineRenderer.material = PositiveColor;
    }
}
