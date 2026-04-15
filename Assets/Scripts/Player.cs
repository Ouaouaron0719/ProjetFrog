using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }


    public PlayerInputSet input {  get; private set; }
    public StateMachine stateMachine;

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public bool canWallSlide { get; private set; } = false;

    public Player_WallSlideState wallSlideState { get; private set; }

    public bool canDash { get; private set; } = false;
    public Player_DashState dashState { get; private set; }

    public bool canWallJump { get; private set; } = false;
    public Player_WallJumpState wallJumpState { get; private set; }



    [Header("Movement Details")]
    public float moveSpeed;
    public float jumpForce = 5;
    public Vector2 wallJumpDir;
    public float inAireMoveMultiplier = .7f;
    public float dashDuration = .25f;
    public float dashSpeed = 20;
    private bool facingLeft = true;
    public int facingDir { get; private set; } = -1;
    public Vector2 moveInput { get; private set; }

    [Header("Dash Details")]
    public float dashCooldown = 1f;
    private float dashCooldownTimer;

    [Header("Collision Detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    public bool groundDetected {  get; private set; }
    public bool wallDetected { get; private set; }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(stateMachine, "idle",this);
        moveState = new Player_MoveState(stateMachine, "move",this);
        jumpState = new Player_JumpState(stateMachine, "jumpFall", this);
        fallState = new Player_FallState(stateMachine, "jumpFall", this);
        dashState = new Player_DashState(stateMachine, "dash", this);
        wallSlideState = new Player_WallSlideState(stateMachine,"wallSlide", this);
        wallJumpState = new Player_WallJumpState(stateMachine, "jumpFall", this);
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }
    private void OnDisable()
    {
        input.Disable();
    }
    private void Start()
    {
        LoadAbilities();
        stateMachine.Initialize(idleState);
    }
    private void Update()
    {
        HandleCollisionDetection();

        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        stateMachine.UpdateActiveState();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFilp(xVelocity);
    }

    private void HandleFilp(float xVelocity)
    {
        if (xVelocity < 0 && facingLeft == false)
            Flip();
        else if (xVelocity > 0 && facingLeft) 
            Flip();
    }
    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingLeft =! facingLeft;
        facingDir *= -1;
    }

    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir,0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (stateMachine.currentState != dashState)
            return;

        BreakableWall wall = collision.gameObject.GetComponent<BreakableWall>();
        if (wall != null)
        {
            wall.Break();
        }
    }
    public void UnlockDash()
    {
        canDash = true;
        if (DatabaseManager.Instance != null)
            DatabaseManager.Instance.SaveAbility("Dash", true);
    }
    public void UnlockWallSlide()
    {
        canWallSlide = true;
        if (DatabaseManager.Instance != null)
            DatabaseManager.Instance.SaveAbility("WallSlide", true);
    }
    public void UnlockWallJump()
    {
        canWallJump = true;
        if (DatabaseManager.Instance != null)
            DatabaseManager.Instance.SaveAbility("WallJump", true);
    }
    public bool CanUseDash()
    {
        return canDash && dashCooldownTimer <= 0f;
    }

    public void StartDashCooldown()
    {
        dashCooldownTimer = dashCooldown;
    }
    private void LoadAbilities()
    {
        if (DatabaseManager.Instance == null)
            return;

        canDash = DatabaseManager.Instance.LoadAbility("Dash");
        canWallSlide = DatabaseManager.Instance.LoadAbility("WallSlide");
        canWallJump = DatabaseManager.Instance.LoadAbility("WallJump");
    }
}
