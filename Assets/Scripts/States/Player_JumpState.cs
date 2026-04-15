using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player_JumpState : Player_AiredState
{
    public Player_JumpState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(rb.linearVelocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if(rb.linearVelocity.y < 0)
            stateMachine.ChangeState(player.fallState);
    }
}
