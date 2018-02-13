using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountTime : MonoBehaviour {
    
    public Text timerText;
    public Text gameOverText;
    public float timer = 30f;
    

    

    private void Start()
    {
        gameOverText.text = "";
        timerText = GetComponent<Text>();  
        
    }



    private void Update()
    {
        TimeTime();
    }

    public void TimeTime()
    {
        timer -= Time.deltaTime;
        {

            timerText.text = timer.ToString("f0");
            if (timer <= 0f)
            {
                gameOverText.text = "GAMER OVER";
                timerText.text = null;
                Time.timeScale = 0;

            }



        }
    }

   
}







