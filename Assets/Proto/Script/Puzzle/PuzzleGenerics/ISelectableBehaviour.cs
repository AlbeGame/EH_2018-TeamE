using UnityEngine;

public interface ISelectableBehaviour  {

    void OnInit(SelectableItem _holder);

    void OnSelect();

    void OnMouseUp();

    void OnMouseDown();
}
