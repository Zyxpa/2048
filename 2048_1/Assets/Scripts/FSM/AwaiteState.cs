using UnityEngine;

public class AwaiteState : BaseState
{ 
    InputController inputController;
    GridController GridCntrl;
    public AwaiteState(FSM fsm, GridController gridController) : base(fsm)
    {
        inputController = new InputController();
        inputController.InputEvent.AddListener(InputHandler);
        GridCntrl = gridController;
    }

    override public void Update()
    {
        inputController.Update();
    }

    void InputHandler(Vector2 inputDirecton)
    {
        Debug.Log("Было нажатие");
        GridCntrl.DoMove(inputDirecton);
    }
}
