using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager<State> : MonoBehaviour where State : Enum
{
    protected Dictionary<State, BaseState<State>> States = new();
    protected BaseState<State> CurrentState;
    protected bool IsTransitionState = false;
     
    void Start()
    {
        CurrentState.EnterState();
    }

    void Update()
    {
        State nextStateKey = CurrentState.GetNextState();

        if (nextStateKey.Equals(CurrentState.StateKey))
            CurrentState.UpdateState(CurrentState);
        else if (!IsTransitionState)
            TransitionToState(nextStateKey);
    }


    public virtual void TransitionToState(State stateKey)
    {
        IsTransitionState = true;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        IsTransitionState = false;
    }

     

    private void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);
    }
}
