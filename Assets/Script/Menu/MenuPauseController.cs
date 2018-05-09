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

    private void Start()
    {
        currentES = FindObjectOfType<EventSystem>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            TogglePauseMenu();
    }


    public void GoMainMenu(int _sceneIndex = 0)
    {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
