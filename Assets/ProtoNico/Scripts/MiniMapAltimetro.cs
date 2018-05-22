using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniMapAltimetro : MonoBehaviour {

    public GameObject mapAltimeter;
    bool isActiveMap;

	void Start () {
        NotActive();
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(2))
        {
            if (isActiveMap)
            {
                NotActive();
            }

            else
            {
                ActiveMap();
            }
        }
    }


    void ActiveMap()
    {
        isActiveMap = true;
        mapAltimeter.SetActive(true);
    }

    void NotActive()
    {
        isActiveMap = false;
        mapAltimeter.SetActive(false);
    }

}
