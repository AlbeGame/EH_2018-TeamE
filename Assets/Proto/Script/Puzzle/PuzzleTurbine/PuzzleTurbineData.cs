using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle Data", fileName = "New Turbine Data")]
public class PuzzleTurbineData : ScriptableObject, IPuzzleData
{
    public int[] EValues = new int[4] { 50, 50, 50, 50 };

    public TurbineButtonData[] ButtonsValues = new TurbineButtonData[8];
}
