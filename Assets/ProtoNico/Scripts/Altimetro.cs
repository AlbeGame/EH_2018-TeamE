using UnityEngine;
using UnityEngine.UI;


public class Altimetro : MonoBehaviour
{
    float multiplier = 1.3f;
    int updateTimer = 0;
    public GameObject ArrowToMove;
    public float DropSpeed = 1.0f;
    public float MaxAltitude = 1000;
    public float currentAltitude;
    
    private int SecondsToMove;
    SelectionRoot gameController;

    private void Start()
    {
        currentAltitude = MaxAltitude;
        if (ArrowToMove == null)
            ArrowToMove = this.gameObject;

        gameController = GetComponent<SelectableBehaviour>().GetRoot().GetComponent<SelectionRoot>();
    }

    private void Update()
    {
        UpdateAltitude();
        GetMoveAltimeter();
       // GetMoveArrowSeconds();
    }

    void UpdateAltitude()
    {
        gameController.NotifyAltitudeUpdate(MaxAltitude, currentAltitude);


        if (currentAltitude <= 0)
        {
            currentAltitude = 0;
            return;
        }

        currentAltitude -= Time.deltaTime * DropSpeed;
        
    }
    
    
    
    void GetMoveAltimeter()
    {
        float currentAngle = (360 * currentAltitude) / MaxAltitude;
        if (currentAltitude == 0)
            currentAngle = 0;

        ArrowToMove.transform.localRotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
    }

    //muove la seconda freccia più sottile al secondo 
    void GetMoveArrowSeconds()
    {
        SecondsToMove = (int)(Time.time % 60);
        gameObject.transform.localRotation = Quaternion.AngleAxis(SecondsToMove, Vector3.back);
    }

    //funzione che aggiunge 0.3 al multiplier per poi moltiplicandolo con il time "attuale in game" per ogni volta che si distrugge un oggetto in scena
    public void AddTime()
    {
         
        currentAltitude *= multiplier;
        multiplier = multiplier + 0.3f;

        
    }

}

