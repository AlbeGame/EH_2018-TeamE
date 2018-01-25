using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour {

    public MenuController MC;

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MC.SelectNext();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MC.SelectPrevious();
        }
    }
}
