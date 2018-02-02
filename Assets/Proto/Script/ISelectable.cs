using UnityEngine;

public interface ISelectable {
    
    SelectionState State { get; set; }

    void Select();
}

public enum SelectionState
{
    Normal,
    Highlighted,
    Pressed
}
