public abstract class BaseState
{
    private FSM _fsm;
    protected BaseState(FSM fsm)
    {
        _fsm = fsm;
    }

    virtual public void Enter() { }
    virtual public void Exit() { }
    virtual public void Update() { }

}
