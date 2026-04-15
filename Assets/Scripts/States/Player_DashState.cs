using UnityEngine;

public class Player_DashState : EntityState
{
    private float originalGravityScale;
    public Player_DashState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        player.StartDashCooldown();

        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }
    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * player.facingDir, 0);

        if (stateTimer < 0)
        {
            if(player.groundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.fallState);
        }
            
            
    }
    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0,0);
        rb.gravityScale = originalGravityScale;
    }
}
