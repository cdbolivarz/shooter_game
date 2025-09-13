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
			animationPlayer.Play(PlayerAnimationEnum.Jump.ToAnimationName());
		}
		else
		{
			animationPlayer.Play(PlayerAnimationEnum.Fall.ToAnimationName());
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
		else if (velocity.Y > 0 && animationPlayer.CurrentAnimation != PlayerAnimationEnum.Fall.ToAnimationName())
		{
			animationPlayer.Play(PlayerAnimationEnum.Fall.ToAnimationName());
		}
	}
	
	public override void HandleInputAction(InputAction action)
	{
		switch (action)
		{
			case InputAction.Jump:
				if (player.Velocity.Y < 0)
				{
					Vector2 velocity = player.Velocity;
					velocity.Y = Mathf.Max(velocity.Y, -200.0f);
					player.Velocity = velocity;
				}
				break;
				
			case InputAction.Shoot:
				animationPlayer.Play(PlayerAnimationEnum.Shoot.ToAnimationName());
				break;
		}
	}
	
	public override void HandleMovement(Vector2 inputDirection)
	{
		Vector2 velocity = player.Velocity;
		velocity.X = inputDirection.X * attributes.airMoveSpeed;
		player.Velocity = velocity;

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
