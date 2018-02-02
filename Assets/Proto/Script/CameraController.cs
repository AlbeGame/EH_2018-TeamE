using DG.Tweening;
using UnityEngine;

[RequireComponent (typeof(Camera))]
public class CameraController : MonoBehaviour {

    public float MovementSpeed = 0.2f;
    Vector3 originalForward;
    Vector3 originalPosition;
    Camera cam;

	// Use this for initialization
	void Start () {
        originalForward = transform.forward;
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
        transform.DOLookAt(_target.forward,MovementSpeed);
        transform.DOMove(_target.position, MovementSpeed);
    }
    /// <summary>
    /// Move the camera toward _targetPosition and rotate it as _forward
    /// </summary>
    /// <param name="_target"></param>
    public void FocusAt(Vector3 _targetPosition, Vector3 _forward)
    {
        transform.DOLookAt(_forward, MovementSpeed);
        transform.DOMove(_targetPosition, MovementSpeed);
    }
    /// <summary>
    /// Move the camera to her original position and orientation
    /// </summary>
    public void FocusReset()
    {
        FocusAt(originalPosition, originalForward);
    }
    #endregion
}
