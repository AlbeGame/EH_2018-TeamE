using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ItemGraphicController : MonoBehaviour
{
    public Material PassiveMat;
    public Material NeutralMat;
    public Material HighlightedMat;
    public Material SelectedMat;
    public Material BrokenMat;
    public Material SolvedMat;

    MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Paint(SelectionState _state)
    {
        switch (_state)
        {
            case SelectionState.Passive:
                meshRenderer.material = PassiveMat;
                break;
            case SelectionState.Neutral:
                meshRenderer.material = NeutralMat;
                break;
            case SelectionState.Highlighted:
                meshRenderer.material = HighlightedMat;
                break;
            case SelectionState.Selected:
                meshRenderer.material = SelectedMat;
                break;
        }
    }

    public void Paint(PuzzleState _state)
    {
        switch (_state)
        {
            case PuzzleState.Unsolved:;
            break;
            case PuzzleState.Broken:
                meshRenderer.material = BrokenMat;
                break;
            case PuzzleState.Solved:
                meshRenderer.material = SolvedMat;
                break;
            default:
                break;
        }
    }
}
