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

        if(data.ParticlesGroup != null)
            data.ParticlesGroup.SetActive(false);

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
                if(data.Lights != null)
                    data.Lights.material = data.EmissiveNegative;
                if (data.ParticlesGroup != null)
                    data.ParticlesGroup.SetActive(false);
                break;
            case PuzzleState.Broken:
                Paint(data.BrokenMat);
                if(data.Lights != null)
                    data.Lights.material = data.EmissiveNegative;
                if (data.ParticlesGroup != null)
                    data.ParticlesGroup.SetActive(true);
                break;
            case PuzzleState.Solved:
                Paint(data.SolvedMat);
                if(data.ParticlesGroup != null)
                    data.ParticlesGroup.SetActive(false);
                if(data.Lights != null)
                    data.Lights.material = data.EmissivePositive;
                break;
            default:
                break;
        }
    }

    public void Paint(Material _mat)
    {
        foreach (Renderer renderer in meshRenderers)
        {
            Material[] newMaterials = renderer.materials;
            for (int i = 0; i < newMaterials.Length; i++)
            {
                if (renderer == data.Lights && i == 0)
                    continue;
                newMaterials[i] = _mat;
            }
            renderer.materials = newMaterials;
        }
    }
}

[System.Serializable]
public class PuzzleGraphicData
{
    [Tooltip("Lista di tutti i renderer figli che verranno ignorati da tutte le variazioni di colore del puzzle")]
    public List<Renderer> DoNotPaintItems = new List<Renderer>();

    [Header("Puzzle Lights")]
    public MeshRenderer Lights;
    public Material EmissivePositive;
    public Material EmissiveNegative;

    [Header("Puzzle Particles")]
    public GameObject ParticlesGroup;

    [Header("Materials"), Space]
    public Material PassiveMat;
    public Material NeutralMat;
    public Material HighlightedMat;
    public Material SelectedMat;
    public Material BrokenMat;
    public Material SolvedMat;
}
