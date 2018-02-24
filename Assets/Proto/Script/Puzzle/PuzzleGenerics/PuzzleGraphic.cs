using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestore della grafica del puzzle. Cambia il materiale a tutti i renderder in parentela, esclusi specifici.
/// </summary>
public class PuzzleGraphic : MonoBehaviour
{
    PuzzleGraphicData data;
    List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    public void Init(PuzzleGraphicData _data)
    {
        data = _data;

        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            if(!renderer.GetComponent<TextMesh>())
                if (!data.DoNotPaintItems.Contains(renderer))
                    meshRenderers.Add(renderer);
        }

        Paint(data.NeutralMat);
    }

    public void Paint(SelectionState _state)
    {
        if (data == null)
            return;

        switch (_state)
        {
            case SelectionState.Passive:
                Paint(data.PassiveMat);
                break;
            case SelectionState.Neutral:
                Paint(data.NeutralMat);
                break;
            case SelectionState.Highlighted:
                Paint(data.HighlightedMat);
                break;
            case SelectionState.Selected:
                Paint(data.SelectedMat);
                break;
            default:
                break;
        }
    }

    public void Paint(PuzzleState _state)
    {
        if (data == null)
            return;

        switch (_state)
        {
            case PuzzleState.Unsolved:;
            break;
            case PuzzleState.Broken:
                Paint(data.BrokenMat);
                break;
            case PuzzleState.Solved:
                Paint(data.SolvedMat);
                break;
            default:
                break;
        }
    }

    public void Paint(Material _mat)
    {
        foreach (Renderer renderer in meshRenderers)
        {
            renderer.material = _mat;
        }
    }
}

[System.Serializable]
public class PuzzleGraphicData
{
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
