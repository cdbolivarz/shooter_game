using ShooterGame.Components;
using ShooterGame.Input;
using ShooterGame.Systems;

namespace ShooterGame.States.Weapon;

public class ShootingState : WeaponStateBase
{
    private bool _toNoShoot;
    private bool _toReload;
    private bool _toSwitch;

    public ShootingState(WeaponSystem weaponSystem) : base(weaponSystem) { }

    public override void Enter()
    {
        _toNoShoot = _toReload = _toSwitch = false;
    }

    public override void Update(float delta)
    {
        WeaponSystem.TryShoot();
        if (WeaponSystem.CurrentWeapon.Ammo.CurrentAmmo <= 0)
            _toNoShoot = true;
    }

    public override void HandleAction(InputAction action)
    {
        AmmoComponent ammo = WeaponSystem.CurrentWeapon.Ammo;

        switch (action)
        {
            case InputAction.Reload:
                if (CanReload(ammo))
                    _toReload = true;
                break;
            case InputAction.SwitchWeapon:
                _toSwitch = true;
                break;
            case InputAction.ShootReleased:
                _toNoShoot = true;
                break;
        }
    }

    public override WeaponStateType? NextStateType
    {
        get
        {
            if (_toNoShoot) return WeaponStateType.NoShooting;
            if (_toReload) return WeaponStateType.Reloading;
            if (_toSwitch) return WeaponStateType.SwitchingWeapon;
            return null;
        }
    }

    private static bool CanReload(AmmoComponent ammo) =>
        ammo.CurrentAmmo < ammo.MaxAmmo && !ammo.IsReloading && ammo.CurrentMagazine > 0;
}
