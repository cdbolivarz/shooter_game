using Godot;
using ShooterGame.Components;
using ShooterGame.Core;
using ShooterGame.Input;
using ShooterGame.States.Player;
using ShooterGame.Systems;

namespace ShooterGame.Entities;

public partial class PlayerController : CharacterBody2D, IEntity, IDamagable
{
    [Export] public float MoveSpeed { get; set; } = 200f;
    [Export] public float AirMoveSpeed { get; set; } = 150f;
    [Export] public float JumpForce { get; set; } = -400f;
    [Export] public float Gravity { get; set; } = 980f;
    [Export] public float MaxFallSpeed { get; set; } = 800f;
    [Export] public int MaxHealth { get; set; } = 100;

    [Export] public AnimationPlayer AnimationPlayer { get; set; }
    [Export] public Sprite2D Sprite { get; set; }
    [Export] public string[] WeaponInventory { get; set; } = new string[] { "m16", "famas", "cannon" };

    public ComponentBag Components { get; } = new();
    public HealthComponent Health => Components.Get<HealthComponent>();
    public WeaponSystem WeaponSystem { get; private set; }
    public PlatformSystem PlatformSystem { get; private set; }

    private StateMachine<PlayerStateType, IPlayerState> _stateMachine;
    private SystemManager _systems;

    public override void _Ready()
    {
        Components.Add(new MovementComponent(MoveSpeed, AirMoveSpeed, JumpForce, Gravity, MaxFallSpeed));
        Components.Add(new HealthComponent(MaxHealth));

        WeaponSystem = new WeaponSystem(this, WeaponInventory);
        PlatformSystem = new PlatformSystem();

        _systems = new SystemManager();
        _systems.Add(WeaponSystem);

        _stateMachine = new StateMachine<PlayerStateType, IPlayerState>(new PlayerStateFactory(this).Create);
        _stateMachine.Initialize(PlayerStateType.Ground);

        InputSystem.OnActionTriggered += OnActionTriggered;
    }

    public override void _ExitTree()
    {
        InputSystem.OnActionTriggered -= OnActionTriggered;
    }

    public override void _Process(double delta)
    {
        InputSystem.ProcessInput();
        _stateMachine.Update((float)delta);
        _systems.Update(delta);

        if (!Health.IsAlive)
            GD.Print("Player is dead");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 input = InputSystem.GetMovementInput();

        UpdateFacing(input);
        _stateMachine.Current?.HandleMovement(input);

        MoveAndSlide();
    }

    private void OnActionTriggered(InputAction action)
    {
        _stateMachine.Current?.HandleInputAction(action);
    }

    private void UpdateFacing(Vector2 input)
    {
        if (input.X == 0)
            return;

        Sprite.FlipH = input.X < 0;
        WeaponSystem.CurrentWeapon?.SetFacing(input.X);
    }
}
