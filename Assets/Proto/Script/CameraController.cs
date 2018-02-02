using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

    public float MovementSpeed = 0.2f;
    Quaternion originalRotation;
    Vector3 originalPosition;
    Camera cam;

    // Use this for initialization
    void Start()
    {
        originalRotation = transform.rotation;
        originalPosition = transform.position;
        cam = GetComponent<Camera>();
    }

    #region API
    /// <summary>
    /// Move the camera toward _target and rotate it as _target.forward
    /// </summary>
    /// <param name="_target"></param>
    public void FocusAt(Transform _target)
    {
        transform.DORotateQuaternion(_target.rotation, MovementSpeed);
        transform.DOMove(_target.position, MovementSpeed);
    }
    /// <summary>
    /// Move the camera toward _targetPosition and rotate it as _forward
    /// </summary>
    /// <param name="_target"></param>
    public void FocusAt(Vector3 _targetPosition, Quaternion _targetRotation)
    {
        transform.DORotateQuaternion(originalRotation, MovementSpeed);
        transform.DOMove(_targetPosition, MovementSpeed);
    }
    /// <summary>
    /// Move the camera to her original position and orientation
    /// </summary>
    public void FocusReset()
    {
        FocusAt(originalPosition, originalRotation);
    }
    #endregion
}