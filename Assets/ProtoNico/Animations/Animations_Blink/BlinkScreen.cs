using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkScreen : MonoBehaviour {
	public TextMesh text;
	private float blinkRate = 0.5f;
	private float _blinkRate;
	private bool isShowingCaret = false;

    
    void Start () {
        _blinkRate = blinkRate;
	}
	
	
	void Update () {
        GetBlink();

	}

    //mostra il trattino se è vero altrimenti no
	void ShowBlinking(){

		if(isShowingCaret){
			
			text.text = text.text.Remove(text.text.Length -1);
            
		}
		else{
		
			text.text = text.text.Insert(text.text.Length,"|");
           
		}

		isShowingCaret = !isShowingCaret;
	}

    //la funzione GetBlink() fa il calcolo quando arriva a zero chiama un altra funzione e mostra blink
    #region API
    //questo fa il calcolo ..quando arriva a zero parte la funzione showBlink e continua ad andare in loop da 0.5 a 0 mostrando il blink.
    public void GetBlink()
    {
        blinkRate -= Time.deltaTime;
        if (blinkRate <= 0f)
        {
            ShowBlinking();       //funzione che mostra blink 
            blinkRate = _blinkRate;
        }
    }

    #endregion
}
