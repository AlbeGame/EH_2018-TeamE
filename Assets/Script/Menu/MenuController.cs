using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject LevelSelectionPanel;
    public GameObject CreditsPanel;
    public GameObject IntroPanel;
    public GameObject TutorialPanel;
    EventSystem currentES;
    AudioMng audioMng;

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

    public void GoToIntro()
    {
        if ((int)GameManager.I_GM.ChosenDifficoulty > 1)
            GoGamePlay(1);
        else
        {
            GoToTutorial();
        }
    }

    public void GoToTutorial()
    {
        GoGamePlay(1);
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

    public void SetDifficoulty(DifficoultyLevel difficoulty)
    {
        GameManager.I_GM.SetDifficultyLevel(difficoulty);
    }
    #endregion

    void ToggleMenu(GameObject _menuObj)
    {

    }

    void ResetEventSystmSelection(GameObject _selection)
    {
        currentES.SetSelectedGameObject(_selection);
    }
}
