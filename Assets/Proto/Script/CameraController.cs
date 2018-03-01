using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    float rangeView = 90.0f;
    public float MovementSpeed = 0.2f;

    bool _isMoveFreeCam;
	public bool isMoveFreeCam
    {
        get { return _isMoveFreeCam; }
        set { _isMoveFreeCam = value; }
    }
    public float sensitivity;

    Quaternion originalRotation;
    Vector3 originalPosition;


    void Start()
    {
        originalRotation = transform.rotation;
        originalPosition = transform.position;
        isMoveFreeCam = true;
    }

    void Update()
    {
        if(isMoveFreeCam)
        RotateCamera();
         
    }


    void RotateCamera()
    {

        float RotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X")*5;
        float RotationX = transform.localEulerAngles.x - Input.GetAxis("Mouse Y")*5;
        gameObject.transform.localEulerAngles = new Vector3(RotationX, RotationY, 0);
        RotationX = Mathf.Clamp(RotationY, -90, 90);


        
       
        // cameraTransform.localRotation *= Quaternion.Euler(0,-newRotationY, 0);

        /* if (transform.localEulerAngles.y > rangeView)
         {
             transform.localEulerAngles = new Vector3(0, 90, 0);
         }
         else
         {
             if (transform.localEulerAngles.y < -rangeView)
             {
                 transform.localEulerAngles = new Vector3(0, -90, 0);
             }
         }*/


    }

    #region API
    /// <summary>
    /// Move the camera toward _target and rotate it as _target.forward
    /// </summary>
    /// <param name="_target"></param>
    public void FocusAt(Transform _target)
    {
        isMoveFreeCam = false;
        transform.DORotateQuaternion(_target.rotation, MovementSpeed);
        transform.DOMove(_target.position, MovementSpeed);
        
    }
    /// <summary>
    /// Move the camera toward _targetPosition and rotate it as _forward
    /// </summary>
    /// <param name="_target"></param>
    public void FocusAt(Vector3 _targetPosition, Quaternion _targetRotation)
    {
        isMoveFreeCam = false;
        transform.DORotateQuaternion(originalRotation, MovementSpeed);
        transform.DOMove(_targetPosition, MovementSpeed);
    }
    /// <summary>
    /// Move the camera to her original position and orientation
    /// </summary>
    public void FocusReset()
    {
        FocusAt(originalPosition, originalRotation);
       isMoveFreeCam=true;
    }
    #endregion
}