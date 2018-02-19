using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Altimetro : MonoBehaviour
{
    public float DropSpeed = 1.0f;
    public float MaxAltitude = 1000;
    float currentAltitude;

    private void Start()
    {
        currentAltitude = MaxAltitude;
    }

    private void Update()
    {
        UpdateAltitude();
        GetMoveAltimeter();
    }

    void UpdateAltitude()
    {
        if(currentAltitude <= 0)
        {
            currentAltitude = 0;
            return;
        }

        currentAltitude -= Time.deltaTime * DropSpeed;
    }


    void GetMoveAltimeter()
    {
        float currentAngle = (360*currentAltitude) / MaxAltitude;
        if (currentAltitude == 0)
            currentAngle = 0;

        transform.localRotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
    }
}
