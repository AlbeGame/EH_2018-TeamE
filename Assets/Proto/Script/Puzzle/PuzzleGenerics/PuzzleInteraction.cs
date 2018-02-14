using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe che gestisce l'interazione con l'utente
/// </summary>
public class PuzzleInteraction : MonoBehaviour {

    PuzzleInteractionData data;

    CameraController camCtrl;


    public void Init(PuzzleInteractionData _data)
    {
        data = _data;
        camCtrl = Camera.main.GetComponent<CameraController>();
    }

    public void CameraFocusCall()
    {
        if (data.CameraFocusPosition)
            camCtrl.FocusAt(data.CameraFocusPosition);
    }
}
