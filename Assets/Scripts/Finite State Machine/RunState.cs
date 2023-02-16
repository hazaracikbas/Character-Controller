using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public RunState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }
}
