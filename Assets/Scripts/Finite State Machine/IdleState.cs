using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private float gravity;
    private bool isJumping;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isSprinting;
    private float speed;

    public IdleState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
        this.player= player;
        this.stateMachine= stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }
}
