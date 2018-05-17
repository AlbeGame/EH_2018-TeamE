using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject LevelSelectionPanel;
    public GameObject menuVolume;
    EventSystem currentES;
    AudioManager audioMng;

    private void Start()
    {
        currentES = FindObjectOfType<EventSystem>();
        audioMng = GameManager.I_GM.AudioManager;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            GoToMainMenu();
    }

    #region API
    public void PlayClickSound()
    {
        audioMng.PlaySound(AudioType.MenuInput);
    }

    public void GoToMainMenu()
    {
        if (!MainMenuPanel.activeSelf)
        {
            MainMenuPanel.SetActive(true);
            //ResetEventSystmSelection(MainMenuPanel.GetComponentInChildren<Button>().gameObject);
        }

        if (LevelSelectionPanel.activeSelf)
            LevelSelectionPanel.SetActive(false);
    }

    public void GoToSelectionMenu()
    {
        if (MainMenuPanel.activeSelf)
            MainMenuPanel.SetActive(false);

        if (!LevelSelectionPanel.activeSelf)
        {
            LevelSelectionPanel.SetActive(true);
            //ResetEventSystmSelection(LevelSelectionPanel.GetComponentInChildren<Button>().gameObject);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoGamePlay(int _sceneIndex)
    {
        //Audio down
        audioMng.FadeAll(0);
        audioMng.Clear();

        SceneManager.LoadScene(_sceneIndex);
    }

    public void SetDifficoulty(int difficoulty)
    {
        GameManager.I_GM.SetDifficultyLevel((DifficoultyLevel) difficoulty);
    }
    #endregion

    void ResetEventSystmSelection(GameObject _selection)
    {
        currentES.SetSelectedGameObject(_selection);
    }
}
