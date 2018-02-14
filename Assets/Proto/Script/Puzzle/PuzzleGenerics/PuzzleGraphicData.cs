using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleGraphicData {

    [Tooltip("Lista di tutti i renderer figli che verranno ignorati da tutte le variazioni di colore del puzzle")]
    public List<Renderer> DoNotPaintItems = new List<Renderer>();

    [Header("Materials"), Space]
    public Material PassiveMat;
    public Material NeutralMat;
    public Material HighlightedMat;
    public Material SelectedMat;
    public Material BrokenMat;
    public Material SolvedMat;
}
