using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public float RotSpeed = 2.5F;
    public float minY = -30.0f;
    public float maxY = 30.0f;
    float RotLeftRight;
    float RotUpDown;
    Vector3 euler;
    public float MovementSpeed = 0.5f;
    bool canMoveFreeCam = true;
    bool _isMoveFreeCam;
    public bool isMoveFreeCam
    {
        get { return _isMoveFreeCam; }
        set
        {
            if (!canMoveFreeCam)
            {
                _isMoveFreeCam = false;
                return;
            }
            else
                _isMoveFreeCam = value;
        }
    }

    GameObject origin;

    void Start()
    {
        euler = new Vector3(0, 90, 0);
        origin = new GameObject("CameraStartingPositon");
        origin.transform.SetParent(transform.parent);
        origin.transform.localPosition = transform.localPosition;
        origin.transform.localRotation = transform.localRotation;
    }

    void Update()
    {
        if (isMoveFreeCam)
            RotateCamera();
    }


    void RotateCamera()
    {

        transform.localEulerAngles = euler;
        RotLeftRight = Input.GetAxis("Mouse X") * RotSpeed;
        RotUpDown = Input.GetAxis("Mouse Y") * RotSpeed;

        euler.y += RotLeftRight;
        euler.x -= RotUpDown;

        if (euler.x >= maxY)
            euler.x = maxY;
        if (euler.x <= minY)
            euler.x = minY;


    }

    #region API
    /// <summary>
    /// Move the camera toward _target and rotate it as _target.forward
    /// </summary>
    /// <param name="_target"></param>
    public void FocusAt(Transform _target)
    {
        //canMoveFreeCam = false;
        //isMoveFreeCam = false;
        //transform.DORotate(_target.rotation.eulerAngles, MovementSpeed);
        ////transform.DORotateQuaternion(_target.rotation, MovementSpeed);
        //transform.DOMove(_target.position, MovementSpeed);
        FocusAt(_target.position, _target.rotation);
    }
    /// <summary>
    /// Move the camera toward _targetPosition and rotate it as _forward
    /// </summary>
    /// <param name="_target"></param>
    public void FocusAt(Vector3 _targetPosition, Quaternion _targetRotation)
    {
        canMoveFreeCam = false;
        isMoveFreeCam = false;
        transform.DORotate(_targetRotation.eulerAngles, MovementSpeed);
        //transform.DORotateQuaternion(originalRotation, MovementSpeed);
        transform.DOMove(_targetPosition, MovementSpeed).OnComplete(()=> { if (_targetPosition == origin.transform.position) canMoveFreeCam = true; });
    }
    /// <summary>
    /// Move the camera to her original position and orientation
    /// </summary>
    public void FocusReset()
    {
        euler = new Vector3(0, 90, 0);
        FocusAt(origin.transform.position, origin.transform.rotation);
    }
    #endregion
}