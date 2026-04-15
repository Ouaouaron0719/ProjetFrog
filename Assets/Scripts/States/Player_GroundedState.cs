using UnityEngine;

public class Player_GroundedState : EntityState
{
    public Player_GroundedState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if(rb.linearVelocity.y < 0)
            stateMachine.ChangeState(player.fallState);
        if (input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.jumpState);
    }
}
