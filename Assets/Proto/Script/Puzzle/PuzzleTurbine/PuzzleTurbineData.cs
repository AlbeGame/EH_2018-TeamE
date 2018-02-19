using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle Data", fileName = "New Turbine Data")]
public class PuzzleTurbineData : ScriptableObject, IPuzzleData
{
    public TurbineButtonData[] ButtonsValues = new TurbineButtonData[8];
}
