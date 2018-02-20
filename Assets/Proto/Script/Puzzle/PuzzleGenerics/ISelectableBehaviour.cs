/// <summary>
/// Specific behaviour that have to be hooked
/// to the generical SelectableAbstract behaviours
/// </summary>
public interface ISelectableBehaviour  {

    void OnInit(SelectableAbstract _holder);

    void OnSelect();
}
