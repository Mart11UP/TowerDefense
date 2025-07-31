using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseState<State> where State : Enum
{
    public BaseState(State key)
    {
        StateKey = key;
    }
    public State StateKey { get; private set; }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void UpdateState(BaseState<State> state) { }
    public abstract State GetNextState();
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnTriggerExit(Collider other) { }
    public virtual void OnTriggerStay(Collider other) { }
}
