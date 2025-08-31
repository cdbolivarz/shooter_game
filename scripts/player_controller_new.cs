using Godot;

public partial class PlayerController : CharacterBody2D
{
    [Export] public float moveSpeed = 200.0f;
    [Export] public float airMoveSpeed = 150.0f;
    [Export] public float jumpForce = -400.0f;
    [Export] public float gravity = 980.0f;
    [Export] public float maxFallSpeed = 800.0f;
    
    private PlayerStateMachine stateMachine;
    private AnimationPlayer animationPlayer;
    private CharacterAttributes attributes;
    private PlayerStateFactory stateFactory;
    
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        attributes = new CharacterAttributes(moveSpeed, airMoveSpeed, jumpForce, gravity, maxFallSpeed);
        stateFactory = new PlayerStateFactory(this, animationPlayer, attributes);
        stateMachine = new PlayerStateMachine(stateFactory);
        
        stateMachine.Initialize(PlayerStateType.Ground);
        
        InputSystem.OnActionTriggered += OnInputActionTriggered;
    }
    
    public override void _ExitTree()
    {
        InputSystem.OnActionTriggered -= OnInputActionTriggered;
    }
    
    public override void _Process(double delta)
    {
        InputSystem.ProcessInput();
        stateMachine.Update((float)delta);
    }
    
    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = InputSystem.GetMovementInput();
        if (inputDirection != Vector2.Zero)
        {
            stateMachine.HandleMovement(inputDirection);
        }
        
        MoveAndSlide();
    }
    
    private void OnInputActionTriggered(InputAction action)
    {
        stateMachine.HandleInputAction(action);
    }
}
