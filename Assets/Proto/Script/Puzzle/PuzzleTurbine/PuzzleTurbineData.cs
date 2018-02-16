using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle Data", fileName = "New Turbine Data")]
public class PuzzleTurbineData : ScriptableObject, IPuzzleData
{
    public int[] EValues = new int[4] { 50, 50, 50, 50 };

    public LabeledButton[] ButtonsValues = new LabeledButton[8];

    [System.Serializable]
    public struct LabeledButton
    {
        public string Label;

        public int E1Modifier;
        public int E2Modifier;
        public int E3Modifier;
        public int E4Modifier;
    }
}
