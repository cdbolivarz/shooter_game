using Godot;
using ShooterGame.Data;
using ShooterGame.Entities;

namespace ShooterGame.Factories;

/// <summary>Builds <see cref="WeaponEntity"/> instances from <see cref="WeaponData"/>.</summary>
public static class WeaponFactory
{
    public static WeaponEntity InstantiateWeapon(Node2D owner, string id)
    {
        WeaponData data = WeaponDatabase.Instance?.GetWeaponData(id);
        if (data == null || data.WeaponScene == null)
            return null;

        WeaponEntity weapon = data.WeaponScene.Instantiate<WeaponEntity>();
        weapon.Id = data.Id;
        weapon.Name = $"Weapon_{data.Id}";

        owner.AddChild(weapon);
        weapon.Position = Vector2.Zero;

        // Ammo
        weapon.Ammo.MaxAmmo = data.MaxAmmo;
        weapon.Ammo.MaxMagazine = data.MaxMagazine;
        weapon.Ammo.ReloadTime = data.ReloadTime;
        weapon.Ammo.ResetToFull();

        // Projectile
        weapon.Projectile.Mode = data.Mode;
        weapon.Projectile.LinearSpeed = data.LinearSpeed;
        weapon.Projectile.ProjectileScene = data.ProjectileScene;
        weapon.Projectile.LifeCycle.Duration = data.Duration;
        weapon.Projectile.LifeCycle.MaxCollitions = data.MaxCollitions;
        weapon.Projectile.LifeCycle.OnExpireEffect = data.OnExpireEffect;
        weapon.Projectile.LifeCycle.OnCollideEffect = data.OnCollideEffect;
        weapon.Projectile.Damage.DamagePerSecond = data.DamagePerSecond;
        weapon.Projectile.Damage.IsAreaEffect = data.IsAreaEffect;
        weapon.Projectile.Damage.AreaRadius = data.AreaRadius;
        weapon.Projectile.Damage.CollitionDamage = data.CollitionDamage;

        // Fire rate
        weapon.FireRate.FireRateDelta = data.FireRateDelta;
        weapon.FireRate.Mode = data.FireRateMode;

        return weapon;
    }
}
