using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Puzzle Data", fileName = "New Autopilot Data")]
public class PuzzleAutopilotData : ScriptableObject, IPuzzleData {

    public List<PartialSolution> Fase1;
    public List<PartialSolution> Fase2;

    [System.Serializable]
    public class PartialSolution {

        public string MonitorSymbol;

        public List<PuzzleAutopilot.InputValue> Solution = new List<PuzzleAutopilot.InputValue>();
    }
}
