using UnityEngine;

public class GameManager : MonoBehaviour {

    static GameManager _iGM;
    public static GameManager I_GM
    {
        get {return _iGM;}
        set
        {
            if (_iGM != null && _iGM != value)
                DestroyImmediate(value.gameObject);
            else
            {
                _iGM = value;
                DontDestroyOnLoad(_iGM.gameObject);
            }
        }
    }
    public LevelSettings DefaultDebug;
    [Header("Settings")]
    public LevelSettings Easy;
    public LevelSettings Medium;
    public LevelSettings Hard;

    public LevelSettings ChosenSetting { get; private set; }

    public AudioManager AudioManager { get; private set; }

    public void Awake()
    {
        I_GM = this;
    }

    private void Start()
    {
        ChosenSetting = DefaultDebug;

        AudioManager = GetComponentInChildren<AudioManager>();
    }

    public void SetDifficultyLevel(DifficoultyLevel _difficoulty)
    {
        switch (_difficoulty)
        {
            case DifficoultyLevel.Easy:
                ChosenSetting = Easy;
                break;
            case DifficoultyLevel.Medium:
                ChosenSetting = Medium;
                break;
            case DifficoultyLevel.Hard:
                ChosenSetting = Hard;
                break;
            default:
                break;
        }
    }
}

public enum DifficoultyLevel
{
    Easy = 1,
    Medium,
    Hard
}