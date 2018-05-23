using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MenuPauseController : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject VictoryPanel;
    public GameObject DefeatPanel;
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
            TogglePauseMenu();
    }


    public void PlayClickSound()
    {
        audioMng.PlaySound(AudioType.MenuInput);
    }

    public void GoMainMenu(int _sceneIndex = 0)
    {
        //Audio down
        audioMng.FadeAll(0);
        audioMng.Clear();

        SceneManager.LoadScene(_sceneIndex);
    }

    public void TogglePauseMenu()
    {
        if (PausePanel.activeSelf)
            PausePanel.SetActive(false);
        else
        {
            PausePanel.SetActive(true);
            VictoryPanel.SetActive(false);
            DefeatPanel.SetActive(false);
            //currentES.SetSelectedGameObject(PausePanel.GetComponentInChildren<Button>().gameObject);
        }
    }

    public void ToggleVictoryMenu()
    {
        if (VictoryPanel.activeSelf)
            VictoryPanel.SetActive(false);
        else
        {
            PausePanel.SetActive(false);
            VictoryPanel.SetActive(true);
            DefeatPanel.SetActive(false);
            //currentES.SetSelectedGameObject(VictoryPanel.GetComponentInChildren<Button>().gameObject);
        }
    }

    public void ToggleDefeatMenu()
    {
        if (DefeatPanel.activeSelf)
            DefeatPanel.SetActive(false);
        else
        {
            PausePanel.SetActive(false);
            VictoryPanel.SetActive(false);
            DefeatPanel.SetActive(true);
            //currentES.SetSelectedGameObject(DefeatPanel.GetComponentInChildren<Button>().gameObject);
        }
    }

    public void RestartLevel()
    {
        //Audio down
        audioMng.FadeAll(0);
        audioMng.Clear();

        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
