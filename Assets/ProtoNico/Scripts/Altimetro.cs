using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Altimetro : MonoBehaviour {

     const float degreeForSecond = 10f;
     public Transform freccia_Altimetro;

    private float time = 1;

  
     private void Update()
     {
         MoveFreccia();
     }


     public void MoveFreccia()
     {
         time += Time.deltaTime;
         freccia_Altimetro.localRotation = Quaternion.Euler(0, 0, time * degreeForSecond);

     }



}
