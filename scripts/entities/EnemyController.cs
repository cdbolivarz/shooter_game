using Godot;
using ShooterGame.Components;
using ShooterGame.Core;
using ShooterGame.Systems;

namespace ShooterGame.Entities;

public partial class EnemyController : CharacterBody2D, IEntity, IDamagable
{
    [Export] public int MaxHealth { get; set; } = 100;
    [Export] public string[] WeaponInventory { get; set; } = new string[] { "m16" };

    public ComponentBag Components { get; } = new();
    public HealthComponent Health => Components.Get<HealthComponent>();
    public WeaponSystem WeaponSystem { get; private set; }

    private SystemManager _systems;

    public override void _Ready()
    {
        Components.Add(new HealthComponent(MaxHealth));

        WeaponSystem = new WeaponSystem(this, WeaponInventory);

        _systems = new SystemManager();
        _systems.Add(WeaponSystem);

        CallDeferred(nameof(InitializeWeapon));
    }

    public override void _Process(double delta)
    {
        _systems.Update(delta);
        WeaponSystem.TryShoot();

        if (!Health.IsAlive)
            QueueFree();
    }

    private void InitializeWeapon()
    {
        WeaponSystem.EquipWeapon();
        WeaponSystem.CurrentWeapon?.SetFacing(-1);
    }
}
