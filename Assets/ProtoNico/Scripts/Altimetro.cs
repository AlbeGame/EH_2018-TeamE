using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Altimetro : MonoBehaviour
{
    public Text gameOverText;
    
    float speedAngle = 1.0f;
    



    void Start()
    {
        gameOverText.text = "";
     
    }


    private void Update()
    {

        GetMoveAltimeter();
    }


    public void GetMoveAltimeter()
    {
        speedAngle += Time.deltaTime * 10;
        speedAngle = Mathf.Clamp(speedAngle, 50, 400);
        transform.localRotation = Quaternion.AngleAxis(speedAngle, Vector3.back);
    }
   


   
   /* void BlockPos()
    {

        if (speedAngle <= -40)
        {
            gameOverText.text = "GAMER OVER";

        }
    }*/
            
            
            

        
    

}
