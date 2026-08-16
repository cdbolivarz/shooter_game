using Godot;
using ShooterGame.Components;

namespace ShooterGame.Data;

[GlobalClass]
public partial class WeaponData : Resource
{
    [Export] public string Id { get; set; } = "";
    [Export] public string DisplayName { get; set; } = "";
    [Export] public PackedScene WeaponScene { get; set; }

    // Projectile
    [Export] public PackedScene ProjectileScene { get; set; }
    [Export] public Vector2 LinearSpeed { get; set; }
    [Export] public ProjectileMode Mode { get; set; } = ProjectileMode.Linear;

    // Lifecycle
    [Export] public float Duration { get; set; } = -1f;
    [Export] public int MaxCollitions { get; set; } = 1;
    [Export] public string OnExpireEffect { get; set; } = "";
    [Export] public string OnCollideEffect { get; set; } = "";

    // Damage
    [Export] public float DamagePerSecond { get; set; }
    [Export] public bool IsAreaEffect { get; set; }
    [Export] public float AreaRadius { get; set; }
    [Export] public float CollitionDamage { get; set; }

    // Fire rate
    [Export] public float FireRateDelta { get; set; } = 0.5f;
    [Export] public FireMode FireRateMode { get; set; } = FireMode.SemiAuto;

    // Ammo
    [Export] public float MaxAmmo { get; set; } = -1;       // -1 = infinite
    [Export] public float ReloadTime { get; set; } = 1f;
    [Export] public float MaxMagazine { get; set; } = -1;   // -1 = infinite
}
