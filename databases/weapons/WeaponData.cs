using Godot;

[GlobalClass]
public partial class WeaponData : Resource
{
    [Export] public string Id { get; set; } = "";
    [Export] public string DisplayName { get; set; } = "";
    [Export] public PackedScene WeaponScene { get; set; } // It should be a Node2D with a child Marker2D node named "Cannon"

    // Projectile properties
    [Export] public PackedScene ProjectileScene { get; set; } // For now rigibody2D
    [Export] public Vector2 LinearSpeed { get; set; }  = new Vector2(0, 0); // needs to be reviewed
    [Export] public string Mode { get; set; } = "Linear"; // *(PROBABLY OTHER .TRES)

    // Projectile/Life cycle
    [Export] public float Duration { get; set; } = -1f; 
    [Export] public float MaxCollitions { get; set; } = 1;
    [Export] public string OnExpireEffect { get; set; } = "";
    [Export] public string OnCollideEffect { get; set; } = "";

    // Projectile/Damage properties
    [Export] public float DamagePerSecond { get; set; } 
    [Export] public bool IsAreaEffect { get; set; } = false;
    [Export] public float AreaRadius { get; set; } = 0.0f; 
    [Export] public float CollitionDamage { get; set; } = 0.0f; 

    // Fire rate properties
    [Export] public float FireRateDelta { get; set; } = 0.5f; // time unity: seconds 
    [Export] public int FireRateMode { get; set; } = 0; // 1 = semi-auto, 2 = full-auto, 3 = burst. We can add more modes later for mods *(PROBABLY OTHER .TRES)

    // Ammo properties
    [Export] public float MaxAmmo { get; set; } =-1; // -1 means infinite ammo
    [Export] public float ReloadTime { get; set; } = 1f;
    [Export] public float MaxMagazine { get; set; } = -1; // -1 means infinite magazine

}