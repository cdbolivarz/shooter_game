using System;
using System.Collections.Generic;
using ShooterGame.Systems;

namespace ShooterGame.States.Weapon;

/// <summary>
/// Creates weapon states on demand. To add a state: write one state class
/// and register it here.
/// </summary>
public class WeaponStateFactory
{
    private readonly Dictionary<WeaponStateType, Func<IWeaponState>> _factories;

    public WeaponStateFactory(WeaponSystem weaponSystem)
    {
        _factories = new Dictionary<WeaponStateType, Func<IWeaponState>>
        {
            [WeaponStateType.Shooting] = () => new ShootingState(weaponSystem),
            [WeaponStateType.NoShooting] = () => new NoShootingState(weaponSystem),
            [WeaponStateType.SwitchingWeapon] = () => new SwitchingWeaponState(weaponSystem),
            [WeaponStateType.Reloading] = () => new ReloadingState(weaponSystem),
        };
    }

    public IWeaponState Create(WeaponStateType type) =>
        _factories.TryGetValue(type, out Func<IWeaponState> factory) ? factory() : null;
}
