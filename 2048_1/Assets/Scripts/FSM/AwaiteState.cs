using UnityEngine;

public class AwaiteState : BaseState
{ 
    InputController inputController;
    public AwaiteState(FSM fsm) : base(fsm)
    {
        inputController = new InputController();
    }

    public override void Enter()
    {
        inputController.InputEvent.AddListener(InputHandler);
    }

    public override void Exit()
    {
        inputController.InputEvent.RemoveListener(InputHandler);
    }

    override public void Update()
    {
        inputController.Update();
    }

    void InputHandler(Vector2 inputDirecton)
    {
        Debug.Log("Было нажатие");
        fsm.SetState<AnimationState>(inputDirecton);
    }
}
