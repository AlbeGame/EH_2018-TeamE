using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

    public float MovementSpeed = 0.2f;
	public bool isMoveFreeCam;
    public float sensitivity;

    Quaternion originalRotation;
    Vector3 originalPosition;


    void Start()
    {
        originalRotation = transform.rotation;
        originalPosition = transform.position;
    }

    void Update()
    {
        if(isMoveFreeCam)
        RotateCamera(); 
    }

    void RotateCamera()
    {
        float newRotationY=transform.localEulerAngles.y +Input.GetAxis("Mouse X")*sensitivity;
        float newRotationX = transform.localEulerAngles.x - Input.GetAxis("Mouse Y")*sensitivity;
        gameObject.transform.localEulerAngles = new Vector3(newRotationX, newRotationY, 0);
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
        isMoveFreeCam = true;
    }
    #endregion
}