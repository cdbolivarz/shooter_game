using Godot;
using System;

public partial class WeaponSystem
{
    public WeaponEntity currentWeapon { get; set; }
    public WeaponStateMachine stateMachine;
    private WeaponStateFactory _stateFactory;

    public WeaponSystem()
    {
        currentWeapon = null;
        _stateFactory = new WeaponStateFactory(this);
        stateMachine = new WeaponStateMachine(_stateFactory);
        stateMachine.Initialize(WeaponStateType.NoShooting);
    }

    public WeaponSystem(WeaponEntity initialWeapon)
    {
        currentWeapon = initialWeapon;
        _stateFactory = new WeaponStateFactory(this);
        stateMachine = new WeaponStateMachine(_stateFactory);
        stateMachine.Initialize(WeaponStateType.NoShooting);
    }

    public void Reload()
    {

        if (
        currentWeapon.Ammo == null || currentWeapon.Ammo.MaxAmmo <= 0 ||
        currentWeapon.Ammo.CurrentMagazine == 0 || currentWeapon.Ammo.CurrentAmmo == currentWeapon.Ammo.MaxAmmo ||
        currentWeapon.Ammo.IsReloading
        )
            return;


        currentWeapon.Ammo.IsReloading = true;


        GD.Print($"[WeaponSystem] Reloading... Current Ammo: {currentWeapon.Ammo.CurrentAmmo}, Current Magazine: {currentWeapon.Ammo.CurrentMagazine}");
        float ammo_to_reload = (float)Math.Min(currentWeapon.Ammo.MaxAmmo - currentWeapon.Ammo.CurrentAmmo, currentWeapon.Ammo.MaxAmmo);
        if (currentWeapon.Ammo.CurrentMagazine >= 0)
            currentWeapon.Ammo.CurrentMagazine -= ammo_to_reload;
        currentWeapon.Ammo.CurrentAmmo += ammo_to_reload;
        GD.Print("Reload complete!");
        currentWeapon.Ammo.IsReloading = false;

    }

    public void TryShoot()
    {

        if (currentWeapon.Projectile == null)
            return;


        if (currentWeapon.Ammo == null)
            return;

        currentWeapon.Ammo.IsReloading = false; // Cancel reload if shooting

        double currentTime = Time.GetTicksMsec() / 1000.0;
        if (currentTime - currentWeapon.lastShotTime < currentWeapon.FireRate.FireRateDelta || currentWeapon.Ammo.CurrentAmmo <= 0)
            return;

        ProjectileSystem.Shoot(currentWeapon.Cannon, currentWeapon.Projectile);

        if (currentWeapon.Ammo.MaxAmmo > 0)
        {
            currentWeapon.Ammo.CurrentAmmo--;
        }
        currentWeapon.lastShotTime = currentTime;
    }

    public void LoadWeapon(Node2D parent, string weaponId)
    {
        var weaponInstance = WeaponFactory.InstantiateWeapon(parent, weaponId);
        if (weaponInstance != null)
        {
            currentWeapon = weaponInstance;
        }
        else
        {
            currentWeapon = null;
        }
    }
    
    public void HandleAction(InputAction action)
    {
        if (currentWeapon == null) return;
        stateMachine?.HandleAction(action);
    }
}