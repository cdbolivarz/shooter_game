using Godot;

public partial class PlayerControllerNew : CharacterBody2D
{
    [Export] public float moveSpeed = 200.0f;
    [Export] public float airMoveSpeed = 150.0f;
    [Export] public float jumpForce = -400.0f;
    [Export] public float gravity = 980.0f;
    [Export] public float maxFallSpeed = 800.0f;
    
    [Export] public AnimationPlayer animationPlayer;
    [Export] public Sprite2D sprite;
    private PlayerStateMachine stateMachine;
    private PlayerStateFactory stateFactory;
    private CharacterAttributes attributes;
    
    public override void _Ready()
    {
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
        
        _HandleSpriteDirection(inputDirection);
        
        stateMachine.HandleMovement(inputDirection);
        
        MoveAndSlide();
    }
    
    private void OnInputActionTriggered(InputAction action)
    {
        stateMachine.HandleInputAction(action);
    }

    private void _HandleSpriteDirection(Vector2 inputDirection)
    {
        if (inputDirection.X != 0)
        {
            sprite.FlipH = inputDirection.X < 0;
        }
    }
}

