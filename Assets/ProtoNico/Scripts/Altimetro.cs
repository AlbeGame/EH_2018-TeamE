using UnityEngine;
using UnityEngine.UI;


public class Altimetro : MonoBehaviour
{
    public float[] Multipliers = new float[6] { .7f, 1f, 1.3f, 1.6f, 2f, 2.33f };
    int currentMultiplayerIndex = 1;
    public GameObject ArrowToMove;
    public float dropSpeed { get { return 1 * Multipliers[currentMultiplayerIndex]; } }
    public float MaxAltitude = 1000;
    public float currentAltitude;
    
    private int SecondsToMove;
    LevelManager gameController;

    private void Start()
    {
        currentAltitude = MaxAltitude;
        if (ArrowToMove == null)
            ArrowToMove = this.gameObject;

        gameController = GetComponent<SelectableBehaviour>().GetRoot().GetComponent<LevelManager>();
    }

    private void Update()
    {
        UpdateAltitude();
        RotateArrow();
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

        currentAltitude -= Time.deltaTime * dropSpeed;
    }
    
    public void Accelerate()
    {
        currentMultiplayerIndex += currentMultiplayerIndex >= Multipliers.Length -1? 0 : 1;
    }

    public void Decelerate(bool goPositive = false)
    {
        if(goPositive)
            currentMultiplayerIndex -= currentMultiplayerIndex <= 0 ? 0 : 1;
        else
            currentMultiplayerIndex -= currentMultiplayerIndex <= 1 ? 0 : 1;
    }
    
    void RotateArrow()
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
}

