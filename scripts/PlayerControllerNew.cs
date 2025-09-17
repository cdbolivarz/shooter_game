using Godot;
using System;

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
    private PlayerSystems playerSystems;

    public override void _Ready()
    {
        playerSystems = new PlayerSystems();
        attributes = new CharacterAttributes(moveSpeed, airMoveSpeed, jumpForce, gravity, maxFallSpeed);
        stateFactory = new PlayerStateFactory(this, animationPlayer, attributes, playerSystems);
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
        playerSystems.Update((float)delta);
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
            if (playerSystems.weaponSystem.currentWeapon != null)
            {
                playerSystems.weaponSystem.currentWeapon.WeaponSprite.FlipH = inputDirection.X < 0;

                // Esto lo cambio despues xD
                Vector2 weapon_position = playerSystems.weaponSystem.currentWeapon.Cannon.Position;
                float x_cannon_positon = inputDirection.X < 0 ? -1 * Math.Abs(weapon_position.X) : Math.Abs(weapon_position.X);
                playerSystems.weaponSystem.currentWeapon.Cannon.Position = new Vector2(x_cannon_positon, weapon_position.Y);

                Vector2 projectile_linear_speed = playerSystems.weaponSystem.currentWeapon.Projectile.LinearSpeed;
                float x_linear_speed = inputDirection.X < 0 ? -1 * Math.Abs(projectile_linear_speed.X) : Math.Abs(projectile_linear_speed.X);
                playerSystems.weaponSystem.currentWeapon.Projectile.LinearSpeed = new Vector2(x_linear_speed, projectile_linear_speed.Y);

                
            }
        }
    }
}

