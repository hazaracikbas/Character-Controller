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

}
