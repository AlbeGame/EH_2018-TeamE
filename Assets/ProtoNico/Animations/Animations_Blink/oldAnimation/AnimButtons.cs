using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimButtons : MonoBehaviour {
	private Animator anim;
   
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
        
	}

    public void OnButtonPressed(){

        anim.SetBool("PushAnimation", true);
    }    

}
