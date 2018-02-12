using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountTime : MonoBehaviour {

    public Text timerText;
    public Text gameOverText;

    private float timer = 5f; 
   

    private void Start()
    {
        gameOverText.text = "";
        timerText = GetComponent<Text>();  
    }



    private void Update()
    {
        timer -= Time.deltaTime;
        {

            timerText.text = timer.ToString("f0");
            if (timer <= 0f)
            {
                gameOverText.text = "GAMER OVER";
                timerText.text = null;
            }



        }
    }



   
}


