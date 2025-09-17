using Godot;

public class GroundState : PlayerStateBase
{
	private bool shouldTransitionToAirborne = false;
	
	public GroundState(CharacterBody2D player, AnimationPlayer animationPlayer, CharacterAttributes attributes, PlayerSystems playerSystems) 
		: base(player, animationPlayer, attributes, playerSystems) { }
	
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
				
			case InputAction.EquipWeapon:
				if (playerSystems.weaponSystem.currentWeapon == null)
					playerSystems.weaponSystem.LoadWeapon(player, "m16");
				break;
			case InputAction.Shoot:
				if (playerSystems.weaponSystem.currentWeapon != null)
					//animationPlayer.Play(PlayerAnimationEnum.Shoot.ToAnimationName());
					playerSystems.weaponSystem.HandleAction(InputAction.Shoot);
				break;
			case InputAction.Reload:
				if (playerSystems.weaponSystem.currentWeapon != null)
					playerSystems.weaponSystem.HandleAction(InputAction.Reload);
				break;
			case InputAction.SwitchWeapon:
				if (playerSystems.weaponSystem.currentWeapon != null)
					playerSystems.weaponSystem.HandleAction(InputAction.SwitchWeapon);
				break;
			case InputAction.ShootReleased:
				if (playerSystems.weaponSystem.currentWeapon != null)
					playerSystems.weaponSystem.HandleAction(InputAction.ShootReleased);
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
