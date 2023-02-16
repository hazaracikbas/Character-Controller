using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintState : State
{
    public SprintState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
        this.player= player;
        this.stateMachine= stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }
}
