using UnityEngine;
using UnityEngine.Events;
public class AnimationState : BaseState
{
    UnityEvent AnimationEnd = new UnityEvent();
    EndMovingEvent endMovingEvent = new EndMovingEvent();

    GridController GridCntrl;
    int countOfStartedAnimation;
    int countOfEndedAnimation;
    public AnimationState(FSM fsm, GridController gridController) : base(fsm)
    {
        GridCntrl = gridController;
        AnimationEnd.AddListener(AnimationEndHandler);
    }

    public override void Enter(object param)
    {
        Vector2 direction = (Vector2)param;
        GridCntrl.DoMove(direction, this.endMovingEvent, AnimationEnd);
        countOfStartedAnimation = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        Debug.Log(countOfEndedAnimation);
    }

    private void AnimationEndHandler()
    {
        countOfEndedAnimation++;
        if(countOfStartedAnimation == countOfStartedAnimation)
            Debug.Log("Cvtyf ");
    }
    public class EndMovingEvent : UnityEvent<int>
    {
    }
}
