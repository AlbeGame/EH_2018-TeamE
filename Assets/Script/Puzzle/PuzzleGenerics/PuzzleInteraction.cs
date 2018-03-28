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

}

[System.Serializable]
public class PuzzleInteractionData
{
    [Tooltip("La posizione che la camera assume nello spazio al momento del focus su questo oggetto")]
    public Transform CameraFocusPosition;
}
