using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class timer : MonoBehaviour {

    float startTime;
    public Text timerText;
  


	
	void Start ()
    {
       startTime = Time.time;
       
    }
	
	
	void Update ()
    {

        float time = Time.time-startTime;
        float minutes = (int)(Time.time / 60f);
        float seconds = (int)(Time.time % 60f);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

    }



}
