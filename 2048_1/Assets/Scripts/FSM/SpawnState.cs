public class SpawnState : BaseState
{
    GridController GridCntrl;
    public SpawnState(FSM fsm, GridController gridController) : base(fsm)
    {
        GridCntrl = gridController;
    }
}
