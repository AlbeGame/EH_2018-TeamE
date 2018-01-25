using DG.Tweening;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float RotationSpeed = 0.2f;
    public float FOVout = 45;
    public float FOVin = 25;
    Vector3 originalForward;
    Camera cam;

	// Use this for initialization
	void Start () {
        originalForward = transform.forward;
        cam = GetComponent<Camera>();
	}
	
    public void FocusAt(Vector3 _target)
    {
        cam.DOFieldOfView(FOVin, RotationSpeed);
        transform.DOLookAt(_target,RotationSpeed);
    }

    public void FocusReset()
    {
        FocusAt(originalForward);
        cam.DOFieldOfView(FOVout, RotationSpeed);
    }
}
