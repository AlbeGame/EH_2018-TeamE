//using DG.Tweening;
using UnityEngine;

public class EMY_camera_shake2 : MonoBehaviour {

    // plus da una parte, minus dall'altra. Su quale asse lo decido in Update
    [Tooltip("Per quanto tempo dura la rotazione in un verso.")]
    public float shake_plus = 1f;
    [Tooltip("Per quanto tempo dura la rotazione nell'altro verso. Se shake_plus > 0 lasciami = 0!")]
    public float shake_minus = 0f;
    [Tooltip("Più questo valore è alto, più lo shake è VELOCE")]
    public float decreaseFactor = 15.0f;
    [Tooltip("Più questo vaore è alto, più è AMPIA la rotazione")]
    public float moltiplicatore_rotazione = 15.0f;

    
    //così si può aggiustare a mano la camera in base all'asse che si vuole far ruotare.
    //non ho modificato i valori del transform.Rotate (vedi Update) perchè la correzione da fare
    //dipende dall'asse su cui si compie la rotazione
    public float X = 0f;
    public float Y = 0f;
    public float Z = 0f;

    private void Start()
    {
        //transform.DOShakeRotation(100, Vector3.forward * moltiplicatore_rotazione);
        transform.Rotate(0 + X, 0 + Y, 0 + Z);
 
    }

    void Update()
    {
        if (shake_plus > 0)
        {
            shake_plus -= Time.deltaTime * decreaseFactor;
            //Spostare la formula [ 1.0f * moltiplicatore_rotazione * Time.deltaTime ] al posto di 0 dell'asse desiderato. gli assi sono: (X, Y, Z). 
            transform.Rotate(0, 0, 1.0f * moltiplicatore_rotazione * Time.deltaTime);
        }
        else if (shake_plus < 0)
        {
            shake_plus = 0f;
            shake_minus = shake_minus + 1.0f;
        }

        if (shake_minus > 0)
        {
            shake_minus -= Time.deltaTime * decreaseFactor;
            //Spostare la formula [ -1.0f * moltiplicatore_rotazione * Time.deltaTime ] al posto di 0 dell'asse desiderato. gli assi sono: (X, Y, Z). 
            transform.Rotate(0, 0, -1.0f * moltiplicatore_rotazione * Time.deltaTime);
        }
        else if (shake_minus < 0)
        {
            shake_minus = 0f;
            shake_plus = shake_plus + 1.0f;
        }
    }
}
