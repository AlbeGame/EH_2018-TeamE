using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPressureData", menuName = "PuzzleData/Pressure")]
public class PuzzlePressureData : ScriptableObject, IPuzzleData
{
    public GameObject Prefab;
    public List<Setup> Setups = new List<Setup>();

    public GameObject GetIPuzzleGO()
    {
        return Prefab;
    }

    [System.Serializable]
    public struct Setup
    {
        public Material ImgToDispaly;
        public PuzzlePressure.ButtonType ButtonToPress;
    }
}
