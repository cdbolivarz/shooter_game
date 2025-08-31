using Godot;

public class AirborneState : PlayerStateBase
{
    private bool shouldTransitionToGround = false;
    
    public AirborneState(CharacterBody2D player, AnimationPlayer animationPlayer, CharacterAttributes attributes) 
        : base(player, animationPlayer, attributes) { }
    
    public override void Enter()
    {
        shouldTransitionToGround = false;
        if (player.Velocity.Y < 0)
        {
            animationPlayer.Play("jump");
        }
        else
        {
            animationPlayer.Play("fall");
        }
    }
    
    public override void Update(float delta)
    {
        Vector2 velocity = player.Velocity;
        velocity.Y += attributes.gravity * delta;
        velocity.Y = Mathf.Min(velocity.Y, attributes.maxFallSpeed);
        player.Velocity = velocity;
        
        if (player.IsOnFloor())
        {
            shouldTransitionToGround = true;
        }
        else if (velocity.Y > 0 && animationPlayer.CurrentAnimation != "fall")
        {
            animationPlayer.Play("fall");
        }
    }
    
    public override void HandleMove(Vector2 inputDirection)
    {
        Vector2 velocity = player.Velocity;
        velocity.X = inputDirection.X * attributes.airMoveSpeed;
        player.Velocity = velocity;
    }
    
    public override void HandleJump()
    {
        if (player.Velocity.Y < 0)
        {
            Vector2 velocity = player.Velocity;
            velocity.Y = Mathf.Max(velocity.Y, -200.0f);
            player.Velocity = velocity;
        }
    }
    
    public override void HandleShoot()
    {
        animationPlayer.Play("air_shoot");
    }
    
    public override PlayerStateType GetNextStateType()
    {
        if (shouldTransitionToGround)
        {
            return PlayerStateType.Ground;
        }
        return PlayerStateType.None;
    }
}
