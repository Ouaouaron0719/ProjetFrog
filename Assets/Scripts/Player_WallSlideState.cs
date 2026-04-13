using UnityEngine;

public class Player_WallSlideState : EntityState
{
    public Player_WallSlideState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Update()
    {
        base.Update();

        if (input.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }
            



        HandleWallSlide();

        if (player.wallDetected == false)
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }
            

        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
            player.Flip();
            return;
        }
           
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);
        else
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * .6f);
    }

}
