using UnityEngine;

public class Player_AiredState : EntityState
{
    public Player_AiredState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }
    public override void Update()
    {
        base.Update();

        if(player.moveInput.x != 0)
            player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.inAireMoveMultiplier),rb.linearVelocity.y);
    }
}
