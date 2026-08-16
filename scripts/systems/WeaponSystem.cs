using System;
using System.Collections.Generic;
using Godot;
using ShooterGame.Components;
using ShooterGame.Core;
using ShooterGame.Entities;
using ShooterGame.Factories;
using ShooterGame.Input;
using ShooterGame.States.Weapon;

namespace ShooterGame.Systems;

/// <summary>
/// Per-owner weapon inventory + fire control. Holds the weapon state machine
/// and delegates shooting/reload/switch behavior to it.
/// </summary>
public class WeaponSystem : ISystem
{
    private readonly Node2D _owner;
    private readonly string[] _inventory;
    private readonly Dictionary<string, WeaponEntity> _weapons = new();
    private readonly StateMachine<WeaponStateType, IWeaponState> _stateMachine;

    public WeaponEntity CurrentWeapon { get; private set; }

    public WeaponSystem(Node2D owner, string[] inventory)
    {
        _owner = owner;
        _inventory = inventory ?? Array.Empty<string>();
        _stateMachine = new StateMachine<WeaponStateType, IWeaponState>(new WeaponStateFactory(this).Create);
        _stateMachine.Initialize(WeaponStateType.NoShooting);
    }

    public void Update(double delta) => _stateMachine.Update((float)delta);

    public void HandleAction(InputAction action)
    {
        if (CurrentWeapon == null)
            return;

        _stateMachine.Current?.HandleAction(action);
    }

    public void EquipWeapon(string weaponId = null)
    {
        if (string.IsNullOrEmpty(weaponId))
            weaponId = GetNextWeaponId();

        if (_inventory.Length == 0 || Array.IndexOf(_inventory, weaponId) == -1 || CurrentWeapon?.Id == weaponId)
            return;

        if (_weapons.TryGetValue(weaponId, out WeaponEntity existing))
        {
            UnloadCurrentWeapon();
            CurrentWeapon = existing;
            CurrentWeapon.Visible = true;
            return;
        }

        WeaponEntity weapon = WeaponFactory.InstantiateWeapon(_owner, weaponId);
        if (weapon != null)
        {
            UnloadCurrentWeapon();
            _weapons[weaponId] = weapon;
            CurrentWeapon = weapon;
        }
    }

    public string GetNextWeaponId()
    {
        if (_inventory.Length == 0)
            return null;

        if (CurrentWeapon == null)
            return _inventory[0];

        int index = Array.IndexOf(_inventory, CurrentWeapon.Id);
        if (index == -1)
            return _inventory[0];

        return _inventory[(index + 1) % _inventory.Length];
    }

    public void TryShoot()
    {
        if (CurrentWeapon == null || CurrentWeapon.Projectile.ProjectileScene == null)
            return;

        AmmoComponent ammo = CurrentWeapon.Ammo;
        ammo.IsReloading = false;

        double now = Time.GetTicksMsec() / 1000.0;
        if (now - CurrentWeapon.LastShotTime < CurrentWeapon.FireRate.FireRateDelta || ammo.CurrentAmmo <= 0)
            return;

        ProjectileSystem.Shoot(CurrentWeapon.Cannon, CurrentWeapon.Projectile);

        if (ammo.MaxAmmo > 0)
            ammo.CurrentAmmo--;

        CurrentWeapon.LastShotTime = now;
    }

    public void Reload()
    {
        if (CurrentWeapon == null)
            return;

        AmmoComponent ammo = CurrentWeapon.Ammo;
        if (ammo.MaxAmmo <= 0 || ammo.CurrentMagazine == 0 || ammo.CurrentAmmo == ammo.MaxAmmo)
            return;

        float amount = Mathf.Min(ammo.MaxAmmo - ammo.CurrentAmmo, ammo.CurrentMagazine);
        if (ammo.CurrentMagazine >= 0)
            ammo.CurrentMagazine -= amount;
        ammo.CurrentAmmo += amount;
    }

    private void UnloadCurrentWeapon()
    {
        if (CurrentWeapon != null)
            CurrentWeapon.Visible = false;
    }
}
