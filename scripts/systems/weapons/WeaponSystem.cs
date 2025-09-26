using Godot;
using System.Collections.Generic;
using System;


public partial class WeaponSystem
{
    // This could be expanded to manage multiple weapons and inventory systems
    public string[] weaponInventory { get; set; }
    public Dictionary<string, WeaponEntity> weaponDictionary { get; set; } = new Dictionary<string, WeaponEntity>();
    public WeaponEntity currentWeapon { get; set; }
    public WeaponStateMachine stateMachine;
    private WeaponStateFactory _stateFactory;

    public WeaponSystem()
    {
        currentWeapon = null;
        weaponInventory = null;
        _stateFactory = new WeaponStateFactory(this);
        stateMachine = new WeaponStateMachine(_stateFactory);
        stateMachine.Initialize(WeaponStateType.NoShooting);
    }

    public WeaponSystem(string[] weaponInventory)
    {
        currentWeapon = null;
        this.weaponInventory = weaponInventory;
        _stateFactory = new WeaponStateFactory(this);
        stateMachine = new WeaponStateMachine(_stateFactory);
        stateMachine.Initialize(WeaponStateType.NoShooting);
    }

    public WeaponSystem(WeaponEntity initialWeapon, string[] weaponInventory)
    {
        this.weaponInventory = weaponInventory;
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


        float ammo_to_reload = (float)Math.Min(currentWeapon.Ammo.MaxAmmo - currentWeapon.Ammo.CurrentAmmo, currentWeapon.Ammo.MaxAmmo);
        if (currentWeapon.Ammo.CurrentMagazine >= 0)
            currentWeapon.Ammo.CurrentMagazine -= ammo_to_reload;
        currentWeapon.Ammo.CurrentAmmo += ammo_to_reload;
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

    public string GetNextWeaponId()
    {
        if (currentWeapon == null) return weaponInventory[0];
        int currentIndex = Array.IndexOf(weaponInventory, currentWeapon.Id);
        if (currentIndex == -1) return weaponInventory[0];
        int nextIndex = (currentIndex + 1) % weaponInventory.Length;
        return weaponInventory[nextIndex];
    }


    public void EquipWeapon(Node2D parent, string weaponId = null)
    {
        if (weaponId == null || weaponId == "")
            weaponId = GetNextWeaponId();

        if (weaponInventory.Length == 0 || weaponInventory == null ||
           Array.IndexOf(weaponInventory, weaponId) == -1 ||
           currentWeapon?.Id == weaponId)
        {
            return;
        }

        if (weaponDictionary.ContainsKey(weaponId))
        {
            UnloadCurrentWeapon(currentWeapon);
            currentWeapon = weaponDictionary[weaponId];
            currentWeapon.Visible = true;
            return;
        }

        var weaponInstance = WeaponFactory.InstantiateWeapon(parent, weaponId);
        if (weaponInstance != null)
        {
            UnloadCurrentWeapon(currentWeapon);
            weaponDictionary[weaponId] = weaponInstance;
            currentWeapon = weaponInstance;
        }
        
    }


    public void UnloadCurrentWeapon(WeaponEntity weapon = null)
    {
        if (weapon != null)
        {
            weapon.Visible = false;
        }
    }


    public void HandleAction(InputAction action)
    {
        if (currentWeapon == null) return;
        stateMachine?.HandleAction(action);
    }
}