using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewAlarmData", menuName = "PuzzleData/Alarm")]
public class PuzzleALARM_Data : ScriptableObject, IPuzzleData
{
    public GameObject GetIPuzzleGO()
    {
        return null;
    }

    public List<PossibileSetup> Setups = new List<PossibileSetup>();

    [System.Serializable]
    public class PossibileSetup
    {
        public List<PartialSolution> CombinationNeeded = new List<PartialSolution>();
    }

    [System.Serializable]
    public class PartialSolution
    {
        public List<PuzzleALARM.InputValue> Solution = new List<PuzzleALARM.InputValue>();
    }
}
