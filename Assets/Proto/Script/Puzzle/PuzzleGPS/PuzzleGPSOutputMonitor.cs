using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGPSOutputMonitor : MonoBehaviour {

    public MeshRenderer MapDisplay;
    Material mapMaterial;
    PuzzleGPSData.GridData data;

    private void Start()
    {
        mapMaterial = new Material(MapDisplay.material);
        MapDisplay.material = mapMaterial;
    }

    public void Init(PuzzleGPSData.GridData _data)
    {
        data = _data;

        mapMaterial.SetTextureScale("_MainTex", data.Scale);
    }

    /// <summary>
    /// It display specific coordinates on the monitor and rotate it with a specific angle
    /// </summary>
    /// <param name="_coordinatesToDisplay"></param>
    public void DisplayAndRotate(Vector2Int _coordinatesToDisplay, float _angle)
    {
        DisplayCoordinates(_coordinatesToDisplay);
        Rotate(_angle);
    }
    /// <summary>
    /// Displays a specific coordinate on the monitor
    /// </summary>
    /// <param name="_coordinatesToDisplay"></param>
    public void DisplayCoordinates(Vector2Int _coordinatesToDisplay)
    {
        Vector2 lowleftCorner = _coordinatesToDisplay - new Vector2(3f, 3f);
        mapMaterial.SetTextureOffset("_MainTex", new Vector2(data.GridTileDimension.x* lowleftCorner.x, data.GridTileDimension.y * lowleftCorner.y));
    }
    /// <summary>
    /// Rotates the monitor by a specific angle
    /// </summary>
    /// <param name="_angle"></param>
    public void Rotate(float _angle)
    {
        MapDisplay.transform.Rotate(Vector3.forward, _angle);
    }
}
