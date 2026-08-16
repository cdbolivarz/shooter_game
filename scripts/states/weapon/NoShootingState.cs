using ShooterGame.Components;
using ShooterGame.Input;
using ShooterGame.Systems;

namespace ShooterGame.States.Weapon;

public class NoShootingState : WeaponStateBase
{
    private bool _toShoot;
    private bool _toReload;
    private bool _toSwitch;

    public NoShootingState(WeaponSystem weaponSystem) : base(weaponSystem) { }

    public override void Enter()
    {
        _toShoot = _toReload = _toSwitch = false;
    }

    public override void HandleAction(InputAction action)
    {
        AmmoComponent ammo = WeaponSystem.CurrentWeapon.Ammo;

        switch (action)
        {
            case InputAction.Shoot:
                ammo.IsReloading = false;
                _toShoot = true;
                break;
            case InputAction.Reload:
                if (CanReload(ammo))
                    _toReload = true;
                break;
            case InputAction.SwitchWeapon:
                _toSwitch = true;
                break;
        }
    }

    public override WeaponStateType? NextStateType
    {
        get
        {
            if (_toShoot) return WeaponStateType.Shooting;
            if (_toReload) return WeaponStateType.Reloading;
            if (_toSwitch) return WeaponStateType.SwitchingWeapon;
            return null;
        }
    }

    private static bool CanReload(AmmoComponent ammo) =>
        ammo.CurrentAmmo < ammo.MaxAmmo && !ammo.IsReloading && ammo.CurrentMagazine > 0;
}
