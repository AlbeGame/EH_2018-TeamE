using UnityEngine;

public class ButtonTagController : MonoBehaviour {

    public TextMesh Label;
    public GameObject ButtonGraphic;

    private void Start()
    {
        Label = GetComponentInChildren<TextMesh>();
    }

    public void OverrideLabel(string _text)
    {
        Label.text = _text;
    }
}
