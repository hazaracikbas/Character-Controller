using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected PlayerController player;
    protected StateMachine stateMachine;
    public State(PlayerController player, StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter() { }
    public virtual void HandleInput() { }
    public virtual void PhysicsUpdate() { }
    public virtual void LogicUpdate() { }
    public virtual void Exit() { }
}
