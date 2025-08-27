using Godot;

public partial class WeaponComponent : Node
{
    [Export] public ProjectileComponent Projectile { get; set; }
    [Export] public FireRateComponent FireRate { get; set; }
    [Export] public AmmoComponent Ammo { get; set; } 
}