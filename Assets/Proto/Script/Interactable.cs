using UnityEngine.UI;
using UnityEngine;

public class Interactable : Button
{
    public KeyCode key;

    Graphic targetGraphic;
    Color normalColor;

    void Awake()
    {
        targetGraphic = GetComponent<Graphic>();
    }

    void Start()
    {
        Realease();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Press();
        }
        else if (Input.GetKeyUp(key))
        {
            Realease();
        }
    }

    void Realease()
    {
        StartColorTween(colors.normalColor, false);
    }

    void Press()
    {
        StartColorTween(colors.pressedColor, false);
        onClick.Invoke();
    }

    void StartColorTween(Color targetColor, bool instant)
    {
        if (targetGraphic == null)
            return;

        targetGraphic.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
    }

    void OnApplicationFocus(bool focus)
    {
        Realease();
    }

    public void LogOnClick()
    {
        Debug.Log("LogOnClick() - " + GetComponentInChildren<Text>().text);
    }
}