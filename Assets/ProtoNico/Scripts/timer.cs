using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    float startTime;
    public Text timerText;



	// Use this for initialization
	void Start () {
       startTime = Time.time;
       
    }
	
	// Update is called once per frame
	void Update () {

        float time = Time.time-startTime;
        //hours = (int)(time / 3600f);
        //string minutes = ((int)time / 60).ToString();
        //string seconds = (time % 60).ToString("f2");
       // timerText.text = minutes + ":" + seconds;
        float minutes = (int)(Time.time / 60f);
        float seconds = (int)(Time.time % 60f);
       
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00"); 
      
        
	}
    
    

}
