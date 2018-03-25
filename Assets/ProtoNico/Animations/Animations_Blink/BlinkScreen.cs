using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlinkScreen : MonoBehaviour {

	public TextMesh text;
	private float blinkRate = 0.5f;
	private float _blinkRate;
	private bool isShowingCaret = false;
    
   void  Start () {
        _blinkRate = blinkRate;
       
    }
	
	
	void Update () {

        GetBlink();
	}

    
	public void ShowBlinking(){

		if(isShowingCaret){
			
			text.text = text.text.Remove(text.text.Length -1);
            
		}
		else{
		
			text.text = text.text.Insert(text.text.Length,"|");
           
		}

		isShowingCaret = !isShowingCaret;
	}

    
    #region API
   
    public void GetBlink()
    {
        blinkRate -= Time.deltaTime;
        if (blinkRate <= 0f)
        {
            ShowBlinking();       
            blinkRate = _blinkRate;
        }
    }

    #endregion
}
