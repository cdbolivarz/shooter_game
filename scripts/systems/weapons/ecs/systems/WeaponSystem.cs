using Godot;
using System;

public partial class WeaponSystem
{
    public static void Reload(WeaponEntity weapon)
    {

        if (
        weapon.Ammo == null || weapon.Ammo.MaxAmmo <= 0 ||
        weapon.Ammo.CurrentMagazine == 0 || weapon.Ammo.CurrentAmmo == weapon.Ammo.MaxAmmo ||
        weapon.Ammo.IsReloading
        )
            return;


        weapon.Ammo.IsReloading = true;


        GD.Print($"[WeaponSystem] Reloading... Current Ammo: {weapon.Ammo.CurrentAmmo}, Current Magazine: {weapon.Ammo.CurrentMagazine}");
        float ammo_to_reload = (float)Math.Min(weapon.Ammo.MaxAmmo - weapon.Ammo.CurrentAmmo, weapon.Ammo.MaxAmmo);
        if (weapon.Ammo.CurrentMagazine >= 0)
            weapon.Ammo.CurrentMagazine -= ammo_to_reload;
        weapon.Ammo.CurrentAmmo += ammo_to_reload;
        GD.Print("Reload complete!");
        weapon.Ammo.IsReloading = false;

    }

    public static void TryShoot(WeaponEntity weapon)
    {

        if (weapon.Projectile == null)
            return;


        if (weapon.Ammo == null)
            return;

        weapon.Ammo.IsReloading = false; // Cancel reload if shooting

        double currentTime = Time.GetTicksMsec() / 1000.0;
        if (currentTime - weapon.lastShotTime < weapon.FireRate.FireRateDelta || weapon.Ammo.CurrentAmmo <= 0)
            return;

        ProjectileSystem.Shoot(weapon.Cannon, weapon.Projectile);

        if (weapon.Ammo.MaxAmmo > 0)
        {
            weapon.Ammo.CurrentAmmo--;
        }
        weapon.lastShotTime = currentTime;
    }
    
    public static WeaponEntity LoadWeapon(Node2D parent, string weaponId)
    {
        var weaponInstance = WeaponFactory.InstantiateWeapon(parent, weaponId);
        if (weaponInstance != null)
        {
            return weaponInstance;
        }
        else
        {
            return null;
        }
    }
}