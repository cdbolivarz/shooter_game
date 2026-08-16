using Godot;
using ShooterGame.Components;
using ShooterGame.Core;

namespace ShooterGame.Entities;

public partial class WeaponEntity : Node2D, IEntity
{
    public ComponentBag Components { get; } = new();

    public string Id { get; set; } = "";

    public ProjectileComponent Projectile => Components.Get<ProjectileComponent>();
    public FireRateComponent FireRate => Components.Get<FireRateComponent>();
    public AmmoComponent Ammo => Components.Get<AmmoComponent>();

    [Export] public Marker2D Cannon { get; set; }
    [Export] public Sprite2D WeaponSprite { get; set; }
    [Export] public AnimationPlayer WeaponAnimation { get; set; }

    public double LastShotTime { get; set; }

    public WeaponEntity()
    {
        Components.Add(new ProjectileComponent());
        Components.Add(new FireRateComponent());
        Components.Add(new AmmoComponent());
    }

    /// <summary>Mirrors the weapon sprite, muzzle and projectile to face the given direction.</summary>
    public void SetFacing(float direction)
    {
        bool faceLeft = direction < 0;

        if (WeaponSprite != null)
            WeaponSprite.FlipH = faceLeft;

        if (Cannon != null)
        {
            Vector2 cannonPos = Cannon.Position;
            Cannon.Position = new Vector2(faceLeft ? -Mathf.Abs(cannonPos.X) : Mathf.Abs(cannonPos.X), cannonPos.Y);
        }

        Vector2 speed = Projectile.LinearSpeed;
        Projectile.LinearSpeed = new Vector2(faceLeft ? -Mathf.Abs(speed.X) : Mathf.Abs(speed.X), speed.Y);
    }
}
