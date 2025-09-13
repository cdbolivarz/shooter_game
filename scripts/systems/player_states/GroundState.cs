using Godot;

public class GroundState : PlayerStateBase
{
	private bool shouldTransitionToAirborne = false;
	
	public GroundState(CharacterBody2D player, AnimationPlayer animationPlayer, CharacterAttributes attributes) 
		: base(player, animationPlayer, attributes) { }
	
	public override void Enter()
	{
		shouldTransitionToAirborne = false;
		animationPlayer.Play(PlayerAnimationEnum.Idle.ToAnimationName());
	}
	
	public override void Update(float delta)
	{
		if (!player.IsOnFloor())
		{
			shouldTransitionToAirborne = true;
		}
	}
	
	public override void HandleInputAction(InputAction action)
	{
		switch (action)
		{
			case InputAction.Jump:
				Vector2 velocity = player.Velocity;
				velocity.Y = attributes.jumpForce;
				player.Velocity = velocity;
				animationPlayer.Play(PlayerAnimationEnum.Jump.ToAnimationName());
				shouldTransitionToAirborne = true;
				break;
				
			case InputAction.Shoot:
				animationPlayer.Play(PlayerAnimationEnum.Shoot.ToAnimationName());
				break;
		}
	}
	
	public override void HandleMovement(Vector2 inputDirection)
	{
		Vector2 velocity = player.Velocity;
		velocity.X = inputDirection.X * attributes.moveSpeed;
		player.Velocity = velocity;
		
		// Only animation depends on input
		if (inputDirection.X != 0)
		{
			animationPlayer.Play(PlayerAnimationEnum.Walk.ToAnimationName());
		}
		else
		{
			animationPlayer.Play(PlayerAnimationEnum.Idle.ToAnimationName());
		}
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
