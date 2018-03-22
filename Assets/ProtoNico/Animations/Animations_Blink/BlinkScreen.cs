using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkScreen : MonoBehaviour {
	public TextMesh text;
	private float blinkRate = 0.5f;
	private float _blinkRate;
	private bool isShowingCaret = false;

    // Use this for initialization
    void Start () {
        _blinkRate = blinkRate;
	}
	
	// Update is called once per frame
	void Update () {
		blinkRate -= Time.deltaTime;
		if(blinkRate <= 0f){
			Blinking();
			blinkRate = _blinkRate;
		}

	}

	void Blinking(){

		if(isShowingCaret){
			//in questo momento stiamo mostrando il caret
			text.text = text.text.Remove(text.text.Length -1);
            
		}
		else{
			//non stiamo mostrando il caret
			text.text = text.text.Insert(text.text.Length,"|");
           
		}

		isShowingCaret = !isShowingCaret;
	}


   

}
