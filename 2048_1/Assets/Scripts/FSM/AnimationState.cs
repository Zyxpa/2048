using System.Diagnostics;
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
    }

    public override void Enter(object param)
    {
        countOfEndedAnimation = 0;
        countOfStartedAnimation = 0;
        AnimationEnd.AddListener(AnimationEndHandler);
        endMovingEvent.AddListener(EndMovingHandler);
        Vector2 direction = (Vector2)param;
        GridCntrl.DoMove(direction, this.endMovingEvent, AnimationEnd);
    }

    public override void Exit()
    {

        AnimationEnd.RemoveAllListeners();
        endMovingEvent.RemoveAllListeners();
    }

    public override void Update()
    {
    }
    private void EndMovingHandler(int _countOfStartedAnimation)
    {
        countOfStartedAnimation = _countOfStartedAnimation;
        //Trace.WriteLine("EndMovingHandler " + countOfStartedAnimation);
        if (_countOfStartedAnimation == 0)
        {
            fsm.SetState<AwaiteState>();
            return;
        }
        if (countOfEndedAnimation == countOfStartedAnimation)
            fsm.SetState<SpawnState>(1);
        
            
    }
    
    private void AnimationEndHandler()
    {
        countOfEndedAnimation++;
        //Trace.WriteLine("AnimationEndHandler " + countOfEndedAnimation + " " + countOfStartedAnimation);
        if (countOfEndedAnimation == countOfStartedAnimation)
            fsm.SetState<SpawnState>(1);
    }
    public class EndMovingEvent : UnityEvent<int>
    {
    }
}
