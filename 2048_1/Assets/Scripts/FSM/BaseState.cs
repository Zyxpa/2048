public abstract class BaseState
{
    protected private FSM fsm;
    protected BaseState(FSM fsm)
    {
        this.fsm = fsm;
    }

    virtual public void Enter() { }
    virtual public void Enter(object param) { }
    virtual public void Exit() { }
    virtual public void Update() { }

}
