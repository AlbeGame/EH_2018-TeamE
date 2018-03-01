﻿using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour {

    #region Singleton Paradigm
    static Debugger _debugger;
    public static Debugger DebugLogger
    {
        get { return _debugger; }
        set
        {
            if (_debugger)
                DestroyImmediate(value.gameObject);
            else
                _debugger = value;
        }   
    }
    private void Awake()
    {
        //Pay attention: property of singleton paradigm
        DebugLogger = this;
    }
    #endregion


    Text textToLog;
    ScrollRect view;

    public void Init () {
        textToLog = GetComponentInChildren<Text>();
        view = GetComponentInChildren<ScrollRect>();

        view.gameObject.SetActive(false);
	}

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            view.gameObject.SetActive(!view.gameObject.activeSelf);
    }

    public void LogText(string _textToLog)
    {
        textToLog.text += "\r\n" + _textToLog;
    }
}