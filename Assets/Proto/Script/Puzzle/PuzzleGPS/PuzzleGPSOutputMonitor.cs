using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGPSOutputMonitor : MonoBehaviour {

    public MeshRenderer MapDisplay;
    Material mapMaterial;
    Vector2Int gridDimensions;
    Vector2 gridTileDimension;

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
        Vector2 lowleftCorner = _coordinatesToDisplay - new Vector2(3.5f, 3.5f);
        mapMaterial.SetTextureOffset("_MainTex", new Vector2(gridTileDimension.x* lowleftCorner.x, gridTileDimension.y * lowleftCorner.y));
    }

    public void Rotate(float _angle)
    {
        MapDisplay.transform.Rotate(Vector3.up, _angle);
    }
}
