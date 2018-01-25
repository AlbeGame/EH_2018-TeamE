using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PauseInGame : MonoBehaviour
{

    
    public static bool gameInPause = false;
    public GameObject gameInPauseUI;

    public MenuVoice[] voices;


    private void Start()
    {
        Resume();

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameInPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        

    }

    void Resume()
    {
        gameInPauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameInPause = false;
    }

    void Pause()
    {
        gameInPauseUI.SetActive(true);
        Time.timeScale = 0f;
        gameInPause = true;
    }




    


   



}
