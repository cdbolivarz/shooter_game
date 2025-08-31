using Godot;

public class GroundState : PlayerStateBase
{
    private bool shouldTransitionToAirborne = false;
    
    public GroundState(CharacterBody2D player, AnimationPlayer animationPlayer, CharacterAttributes attributes) 
        : base(player, animationPlayer, attributes) { }
    
    public override void Enter()
    {
        shouldTransitionToAirborne = false;
        animationPlayer.Play("idle");
    }
    
    public override void Update(float delta)
    {
        if (!player.IsOnFloor())
        {
            shouldTransitionToAirborne = true;
        }
    }
    
    public override void HandleMove(Vector2 inputDirection)
    {
        Vector2 velocity = player.Velocity;
        velocity.X = inputDirection.X * attributes.moveSpeed;
        player.Velocity = velocity;
        
        if (inputDirection.X != 0)
        {
            animationPlayer.Play("walk");
        }
        else
        {
            animationPlayer.Play("idle");
        }
    }
    
    public override void HandleJump()
    {
        Vector2 velocity = player.Velocity;
        velocity.Y = attributes.jumpForce;
        player.Velocity = velocity;
        
        animationPlayer.Play("jump");
        shouldTransitionToAirborne = true;
    }
    
    public override void HandleShoot()
    {
        animationPlayer.Play("shoot");
    }
    
    public override PlayerStateType GetNextStateType()
    {
        if (shouldTransitionToAirborne)
        {
            return PlayerStateType.Airborne;
        }
        return PlayerStateType.None;
    }
}
