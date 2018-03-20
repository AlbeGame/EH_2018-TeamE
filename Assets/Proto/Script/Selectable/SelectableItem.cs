/// Container class with the generical behavior of a selectable.
/// It builds a PuzzleGraphic and a PuzzleInteraction on his own GameObject if found none.
/// </summary>
public class SelectableItem : SelectableAbstract
{
    public PuzzleGraphicData GraphicData;
    protected PuzzleGraphic graphicCtrl;
   
    public PuzzleInteractionData InteractionData;
    protected PuzzleInteraction interactionCtrl;

    protected override void OnInitBegin(SelectableAbstract _parent)
    {
        //Graphic Controller
        graphicCtrl = GetComponent<PuzzleGraphic>();
        if (graphicCtrl == null)
            graphicCtrl = gameObject.AddComponent<PuzzleGraphic>();
        graphicCtrl.Init(GraphicData);

        //Interaction Controller
        interactionCtrl = GetComponent<PuzzleInteraction>();
        if (interactionCtrl == null)
            interactionCtrl = gameObject.AddComponent<PuzzleInteraction>();
        interactionCtrl.Init(InteractionData);
        
    }

    protected override void OnSelect()
    {
        interactionCtrl.CameraFocusCall();
    }

    protected override void OnStateChange(SelectionState _state)
    {
        if (graphicCtrl)
            graphicCtrl.Paint(_state);
    }
}
