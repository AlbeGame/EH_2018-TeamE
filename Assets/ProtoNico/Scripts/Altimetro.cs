using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altimetro : MonoBehaviour {


    public GameObject altimetro;

    public float angleMin = 1;
    public float angleMax = 10;
    public float actualAltitude;
    public float altitudeHigh = 20;
    public float altitudeLow = 2;





    private void Update()
    {
        transform.eulerAngles = new Vector3(angleMin,angleMax,Time.deltaTime);
        float needleAngle = Mathf.Lerp(angleMin, angleMax, actualAltitude / (altitudeHigh - altitudeLow));

    }






}
