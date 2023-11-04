public class AnimationState : BaseState
{
    GridController GridCntrl;
    public AnimationState(FSM fsm, GridController gridController) : base(fsm)
    {
        GridCntrl = gridController;
    }
}
