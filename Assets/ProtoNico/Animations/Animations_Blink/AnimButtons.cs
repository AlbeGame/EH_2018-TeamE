using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimButtons : MonoBehaviour {
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnButtonPressed(){
		anim.SetTrigger("PushAnimation");
	}
}
