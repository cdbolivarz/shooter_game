using ShooterGame.Components;
using ShooterGame.Input;
using ShooterGame.Systems;

namespace ShooterGame.States.Weapon;

public class ReloadingState : WeaponStateBase
{
    private float _reloadTimer;
    private bool _toNoShoot;
    private bool _toShoot;
    private bool _toSwitch;

    public ReloadingState(WeaponSystem weaponSystem) : base(weaponSystem) { }

    public override void Enter()
    {
        _toNoShoot = _toShoot = _toSwitch = false;

        AmmoComponent ammo = WeaponSystem.CurrentWeapon.Ammo;
        ammo.IsReloading = true;
        _reloadTimer = ammo.ReloadTime;
    }

    public override void Exit()
    {
        WeaponSystem.CurrentWeapon.Ammo.IsReloading = false;
    }

    public override void Update(float delta)
    {
        _reloadTimer -= delta;
        if (_reloadTimer <= 0)
        {
            WeaponSystem.Reload();
            _toNoShoot = true;
        }
    }

    public override void HandleAction(InputAction action)
    {
        switch (action)
        {
            case InputAction.Shoot:
                if (WeaponSystem.CurrentWeapon.Ammo.CurrentAmmo > 0)
                    _toShoot = true;
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
            if (_toNoShoot) return WeaponStateType.NoShooting;
            if (_toShoot) return WeaponStateType.Shooting;
            if (_toSwitch) return WeaponStateType.SwitchingWeapon;
            return null;
        }
    }
}
