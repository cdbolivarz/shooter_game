using Godot;

public partial class WeaponEntity : Node2D
{
    public ProjectileComponent Projectile { get; set; } = new ProjectileComponent();
    public FireRateComponent FireRate { get; set; } = new FireRateComponent();
    public AmmoComponent Ammo { get; set; } = new AmmoComponent();
    [Export] public Marker2D Cannon { get; set; }
    [Export] public Sprite2D WeaponSprite { get; set; }
    [Export] public AnimationPlayer WeaponAnimation { get; set; }
    public double lastShotTime = 0;

    public WeaponEntity() {}


}