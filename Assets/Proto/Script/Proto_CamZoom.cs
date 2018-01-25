using UnityEngine;

public class Proto_CamZoom : MonoBehaviour {

    public Material Unselected;
    public Material Selected;

    bool _isMO;
    bool isMouseOver { get { return _isMO; }
        set
        {
            if (_isMO == value)
                return;

            _isMO = value;
            if (!isSelected)
            {
                if(isMouseOver)    
                    mr.material = Selected;
                else
                    mr.material = Unselected;
            }

        }
    }

    bool _isSel;
    bool isSelected {
        get { return _isSel; }
        set
        {
            if (_isSel == value)
                return;

            _isSel = value;
            isMouseOver = false;
            if(isSelected)
                camCtrl.FocusAt(transform.position);
            else
                camCtrl.FocusReset();
        }
    }

    MeshRenderer mr;
    CameraControl camCtrl;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        camCtrl = Camera.main.GetComponent<CameraControl>();
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && isMouseOver)
            isSelected = true;
        if (Input.GetMouseButtonDown(1) && !isMouseOver)
            isSelected = false;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }
}
