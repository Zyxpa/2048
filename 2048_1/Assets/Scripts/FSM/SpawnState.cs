public class SpawnState : BaseState
{
    GridController GridCntrl;
    public SpawnState(FSM fsm, GridController gridController) : base(fsm)
    {
        GridCntrl = gridController;
    }

    public override void Enter(object param)
    {
        int countOfSpawn = (int)param;
        for(int i = 0; i < countOfSpawn; i++)
            GridCntrl.SpawnTile();
        
        fsm.SetState<AwaiteState>();
    }
}
