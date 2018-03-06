using UnityEngine;
using UnityEngine.UI;


public class Altimetro : MonoBehaviour
{
    int updateTimer = 0;
    public GameObject ArrowToMove;
    public float DropSpeed = 1.0f;
    public float MaxAltitude = 1000;
    public float currentAltitude;

    private void Start()
    {
        currentAltitude = MaxAltitude;
        if (ArrowToMove == null)
            ArrowToMove = this.gameObject;
    }

    private void Update()
    {
        UpdateAltitude();
        GetMoveAltimeter();
    }

    void UpdateAltitude()
    {
        if (currentAltitude <= 0)
        {
            currentAltitude = 0;
            return;
        }

        currentAltitude -= Time.deltaTime * DropSpeed;
        
    }
    
    //function for add speed at dropspeed...i must see still
    public void AddTime()
    {
        DropSpeed += 3.5F;
    }

    void GetMoveAltimeter()
    {
        float currentAngle = (360 * currentAltitude) / MaxAltitude;
        if (currentAltitude == 0)
            currentAngle = 0;

        ArrowToMove.transform.localRotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
    }
}

