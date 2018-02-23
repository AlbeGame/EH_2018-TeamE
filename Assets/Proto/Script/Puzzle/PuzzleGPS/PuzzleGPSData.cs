using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGPSData", menuName = "PuzzleData/GPS")]
public class PuzzleGPSData : ScriptableObject, IPuzzleData {
    public List<Vector2Int> PossibleCoordinates = new List<Vector2Int>();

    public Vector2Int GridDimension;
}
