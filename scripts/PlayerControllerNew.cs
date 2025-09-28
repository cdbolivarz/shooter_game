using Godot;
using System;

public partial class PlayerControllerNew : CharacterBody2D, IDamagable, IWeapons, IPlatforms
{
    [Export] public float moveSpeed = 200.0f;
    [Export] public float airMoveSpeed = 150.0f;
    [Export] public float jumpForce = -400.0f;
    [Export] public float gravity = 980.0f;
    [Export] public float maxFallSpeed = 800.0f;
    [Export] public int MaxHealth { get; set; } = 100;

    
    [Export] public AnimationPlayer animationPlayer;
    [Export] public Sprite2D sprite;
    [Export] public string[] weaponInventory = new string[] { "m16", "famas", "cannon" };
    private PlayerStateMachine stateMachine;
    private PlayerStateFactory stateFactory;
    private CharacterAttributes attributes;
    public WeaponSystem weaponSystem { get; set; }
    public PlatformSystem platformSystem { get; set; }
    public DamageSystem damageSystem { get; set; }
    private PlayerSystems Systems;

    public override void _Ready()
    {
        weaponSystem = new WeaponSystem(weaponInventory);
        platformSystem = new PlatformSystem();
        HealthComponent healthComponent = new HealthComponent(MaxHealth);
        damageSystem = new DamageSystem(healthComponent);

        Systems = new PlayerSystems(weaponSystem, platformSystem, damageSystem);
        attributes = new CharacterAttributes(moveSpeed, airMoveSpeed, jumpForce, gravity, maxFallSpeed, healthComponent);
        stateFactory = new PlayerStateFactory(this, animationPlayer, attributes, Systems);
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
        Systems.Update((float)delta);

        if (!attributes.healthComponent.IsAlive)
        {
            GD.Print("Player is dead");
            // Reload or Game Over.
        }
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
    
    public CharacterAttributes GetAttributes()
    {
        return attributes;
    }



    private void _HandleSpriteDirection(Vector2 inputDirection)
    {
        if (inputDirection.X != 0)
        {
            sprite.FlipH = inputDirection.X < 0;
            if (Systems.weaponSystem.currentWeapon != null)
            {
                Systems.weaponSystem.currentWeapon.WeaponSprite.FlipH = inputDirection.X < 0;

                // Esto lo cambio despues xD
                Vector2 weapon_position = Systems.weaponSystem.currentWeapon.Cannon.Position;
                float x_cannon_positon = inputDirection.X < 0 ? -1 * Math.Abs(weapon_position.X) : Math.Abs(weapon_position.X);
                Systems.weaponSystem.currentWeapon.Cannon.Position = new Vector2(x_cannon_positon, weapon_position.Y);

                Vector2 projectile_linear_speed = Systems.weaponSystem.currentWeapon.Projectile.LinearSpeed;
                float x_linear_speed = inputDirection.X < 0 ? -1 * Math.Abs(projectile_linear_speed.X) : Math.Abs(projectile_linear_speed.X);
                Systems.weaponSystem.currentWeapon.Projectile.LinearSpeed = new Vector2(x_linear_speed, projectile_linear_speed.Y);


            }
        }
    }
}

