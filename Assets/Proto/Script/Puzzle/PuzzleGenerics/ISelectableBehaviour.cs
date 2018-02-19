using UnityEngine;

public interface ISelectableBehaviour  {

    void OnInit(SelectableAbstract _holder);

    void OnSelect();

    void OnMouseUp();

    void OnMouseDown();
}
