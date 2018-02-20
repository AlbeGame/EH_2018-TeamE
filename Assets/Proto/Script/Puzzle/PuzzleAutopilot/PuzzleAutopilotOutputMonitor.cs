using DG.Tweening;
using UnityEngine;

public class PuzzleAutopilotOutputMonitor : MonoBehaviour {

    public Material[] materialsCDEHIJ = new Material[6];

    public MeshRenderer Icon;

    public void ToggleOnOff(bool isOn = true) {
        if(!isOn)
            Icon.material.DOBlendableColor(Icon.material.color * new Color(1, 1, 1, 0), 0);
        else
            Icon.material.DOBlendableColor(Icon.material.color * new Color(1, 1, 1, 1), 0);
    }

    public void SetMaterial(int index) {
        Icon.material = materialsCDEHIJ[index];
    }
}
