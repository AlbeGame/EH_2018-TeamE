using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGPSOutputMonitor : MonoBehaviour {

    public MeshRenderer MapDisplay;
    public Material mapMaterial;
    public Vector2Int gridDimensions;
    public Vector2 gridTileDimension;

    private void Start()
    {
        mapMaterial = MapDisplay.material;
    }

    public void Init(Vector2Int _gridDimension)
    {
        gridDimensions = _gridDimension;
        gridTileDimension = new Vector2Int(1 / gridDimensions.x, 1 / gridDimensions.y);
        mapMaterial.SetTextureScale("_MainTex", new Vector2(6 / gridDimensions.x, 6 / gridDimensions.y));
    }

    public void DisplayCoordinates(Vector2Int _coordinatesToDisplay)
    {
        mapMaterial.SetTextureOffset("_MainTex", new Vector2(gridTileDimension.x*_coordinatesToDisplay.x, gridTileDimension.y * _coordinatesToDisplay.y));
    }
}
