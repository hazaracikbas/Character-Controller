using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
  public IdleState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
  {
    this.player = player;
    this.stateMachine = stateMachine;
  }

    public override void Enter()
    {
        base.Enter();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();



    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit() 
    { 
        base.Exit();
    }
}
