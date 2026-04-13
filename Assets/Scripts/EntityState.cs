using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator anim;
    protected Rigidbody2D rb;
    protected PlayerInputSet input;

    protected float stateTimer;

    public EntityState(StateMachine stateMachine, string animBoolName, Player player)
    {
        this.player = player;
        this.stateMachine = stateMachine;   
        this.animBoolName = animBoolName;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Update() 
    {
        stateTimer -= Time.deltaTime;
        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        if (player.CanUseDash() && input.Player.Dash.WasPressedThisFrame())
            stateMachine.ChangeState(player.dashState);
    }
    public virtual void Exit() 
    {
        player.anim.SetBool(animBoolName, false);
    }
}
