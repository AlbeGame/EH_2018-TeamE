using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSettingData", menuName = "LevelSetting/NewSetup")]
public class LevelSettings : ScriptableObject {

    public int TotalPuzzles = 3;
    public int PuzzlesNeededToWin = 2;

    public PuzzleALARM_Data Alarm_Data;
    public List<ScriptableObject> PuzzleDatas = new List<ScriptableObject>();
    public List<GameObject> FillingObjects = new List<GameObject>();
}
