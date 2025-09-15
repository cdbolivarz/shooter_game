using Godot;

public class WeaponFactory
// This class is responsible for creating weapon instances based on WeaponData, cache can be added here
{

    public static WeaponEntity InstantiateWeapon(Node2D entity, string id)
    {
        var _db = WeaponDatabase.GetInstance();
        var data = _db.GetWeaponData(id);
        if (data == null) return null;

        // Should Nodo2D with Material Sprite2D, and "Cannon" Marker2D as child
        var weapon_scene = data.WeaponScene.Instantiate<Node2D>();

        entity.AddChild(weapon_scene);
        weapon_scene.GlobalPosition = entity.GlobalPosition;
        weapon_scene.Position = new Vector2(0, 0);

        var weapon = entity.GetNode<WeaponEntity>("Weapon");

        // AmmoComponent
        weapon.Ammo.MaxAmmo = data.MaxAmmo;
        weapon.Ammo.MaxMagazine = data.MaxMagazine;
        weapon.Ammo.ReloadTime = data.ReloadTime;
        
        // ProjectileComponent
        weapon.Projectile.Mode = data.Mode;
        weapon.Projectile.LinearSpeed = data.LinearSpeed;
        weapon.Projectile.ProjectileScene = data.ProjectileScene;
        // DamageComponent and LifeCycleComponent
        weapon.Projectile.LifeCycle.Duration = data.Duration;
        weapon.Projectile.LifeCycle.MaxCollitions = (int)data.MaxCollitions;
        weapon.Projectile.LifeCycle.OnExpireEffect = data.OnExpireEffect;
        weapon.Projectile.LifeCycle.OnCollideEffect = data.OnCollideEffect;
        weapon.Projectile.Damage.DamagePerSecond = data.DamagePerSecond;
        weapon.Projectile.Damage.IsAreaEffect = data.IsAreaEffect;
        weapon.Projectile.Damage.AreaRadius = data.AreaRadius;
        weapon.Projectile.Damage.CollitionDamage = data.CollitionDamage;

        // FireRateComponent
        weapon.FireRate.FireRateDelta = data.FireRateDelta;
        weapon.FireRate.Mode = data.FireRateMode;
        

        return weapon;
    }
}