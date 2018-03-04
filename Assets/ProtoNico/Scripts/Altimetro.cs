using UnityEngine;
using UnityEngine.UI;

public class Altimetro : MonoBehaviour
{
    public Text GameOverText;
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
            GameOverText.text = "GAME OVER";
            Time.timeScale = 0f;
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
}

