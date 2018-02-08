using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestProva : MonoBehaviour
{
    public GameObject PanelGioca, PanelExit;
  


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& !PanelExit.activeInHierarchy)
        {

            ToggleMenu(PanelGioca);

        }
    }



    public void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeInHierarchy);

    }


    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void ReturnGame(string name)
    {
        Application.LoadLevel(name);
    }

}