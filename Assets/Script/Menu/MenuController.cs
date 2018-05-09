using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject LevelSelectionPanel;
    public GameObject menuVolume;
    EventSystem currentES;

    private void Start()
    {
        currentES = FindObjectOfType<EventSystem>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            GoToMainMenu();
    }

    #region API
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

    //public void GoToMenuVolume()// sto provando
    //{
    //    if (LevelSelectionPanel.activeSelf)
    //        LevelSelectionPanel.SetActive(false);

    //        if (!menuVolume.activeSelf)
    //        {
    //            menuVolume.SetActive(true);
    //            ResetEventSystmSelection(menuVolume.GetComponentInChildren<Button>().gameObject);
    //        }


    //}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoGamePlay(int _sceneIndex)
    {
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
