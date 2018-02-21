using UnityEngine;

[CreateAssetMenu(fileName = "NewTurbineData", menuName = "PuzzleData/Turbine")]
public class PuzzleTurbineData : ScriptableObject, IPuzzleData
{
    public TurbineButtonData[] ButtonsValues = new TurbineButtonData[8];
}
