using UnityEngine;

public class AwaiteState : BaseState
{ 
    InputController inputController;
    public AwaiteState(FSM fsm, InputController _inputController) : base(fsm)
    {
        inputController = _inputController;
    }

    public override void Enter()
    {
        inputController.swipeAction.AddListener(InputHandler);
    }

    public override void Exit()
    {
        inputController.swipeAction.RemoveListener(InputHandler);
    }

    void InputHandler(Vector2 inputDirecton)
    {
        fsm.SetState<AnimationState>(inputDirecton);
    }
}
