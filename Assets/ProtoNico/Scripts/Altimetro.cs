using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Altimetro : MonoBehaviour
{

    const float degreeForSecond = 10f;

    public Transform freccia_Altimetro;
    public Text gameOverText;

    private float timePos = -40.0f;
    private float timer = 35;

    void Start()
    {
        gameOverText.text = "";
    }


    private void Update()
    {
        GetCurrentTime();
        DecreaseTime();

    }


    public void GetCurrentTime()
    {
        timePos += Time.deltaTime;
        freccia_Altimetro.localRotation = Quaternion.Euler(0, 0, timePos * degreeForSecond);

    }

    public void DecreaseTime()
    {
        timer -= Time.deltaTime;
        {


            if (timer <= 0f)
            {
                gameOverText.text = "GAMER OVER";
                Time.timeScale = 0;

            }



        }
    }

}
