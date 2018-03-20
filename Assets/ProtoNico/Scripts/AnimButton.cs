using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimButton : MonoBehaviour {

    Animator anim;
    public bool PushIdle = false;
    public bool PushDown = false;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        PushIdle = false;
        PushDown = false;


    }
	


	// Update is called once per frame
	void Update () {
    

	}

    //funzione che chiama la variabile booleano e parametro nell'animator per quando premi col mouse e lasci andare
    public void Select()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushDown = true;
            PushIdle = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            PushIdle = true;
            PushDown = false;
        }
        if (PushIdle == true)
        {
            anim.SetBool("PushIdle", true);
            anim.SetBool("PushDown", false);
        }
        if (PushDown == true)
        {
            anim.SetBool("PushDown", true);
            anim.SetBool("PushIdle", false);
        }
    }

}

