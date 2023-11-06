using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
    BaseState curentState;
    
    void Update()
    {
        //Debug.Log(curentState);
        curentState?.Update();
    }

    public void AddState<T>( T newState) where T : BaseState
    {
        if(!states.ContainsKey(typeof(T)))
            states.Add( typeof(T), newState);
    }
    public void SetState<T>() where T : BaseState
    {
        if (curentState?.GetType() == typeof(T))
            return;

        if (!states.TryGetValue(typeof(T), out var newState))
            return;

        curentState?.Exit();
        curentState = newState;
        curentState.Enter();
    }

    public void SetState<T>(object param) where T : BaseState
    {
        if (curentState?.GetType() == typeof(T))
            return;

        if (!states.TryGetValue(typeof(T), out var newState))
            return;

        curentState?.Exit();
        curentState = newState;
        curentState.Enter(param);
    }
}
