using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking_Text : BlinkScreen {


    
    TextMesh text;
    private bool blinkFalse = false;
    private bool blinkTrue = false;

    


   

    public bool BlinkFalse
    {
        get { return BlinkFalse; }
    }

    public bool BlinkTrue
    {
        get { return blinkTrue; }
    }



    public void ShowBlink()
    {
        blinkTrue = true;
    }


    public void DontShowBlink()
    {
        blinkFalse = true;
    }

}
