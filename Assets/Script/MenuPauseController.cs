using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MenuPauseController : MonoBehaviour
{
    public GameObject PausePanel;
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
            currentES.SetSelectedGameObject(PausePanel.GetComponentInChildren<Button>().gameObject);
        }
    }
}
