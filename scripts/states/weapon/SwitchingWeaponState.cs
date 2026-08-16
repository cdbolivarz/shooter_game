using ShooterGame.Components;
using ShooterGame.Input;
using ShooterGame.Systems;

namespace ShooterGame.States.Weapon;

public class SwitchingWeaponState : WeaponStateBase
{
    private const float SwitchTime = 0.25f;

    private float _switchTimer;
    private bool _toShoot;
    private bool _toReload;
    private bool _toNoShoot;

    public SwitchingWeaponState(WeaponSystem weaponSystem) : base(weaponSystem) { }

    public override void Enter()
    {
        _toShoot = _toReload = _toNoShoot = false;
        _switchTimer = SwitchTime;
    }

    public override void Update(float delta)
    {
        _switchTimer -= delta;
        if (_switchTimer <= 0)
        {
            WeaponSystem.EquipWeapon();
            _toNoShoot = true;
        }
    }

    public override void HandleAction(InputAction action)
    {
        AmmoComponent ammo = WeaponSystem.CurrentWeapon.Ammo;

        switch (action)
        {
            case InputAction.Shoot:
                _toShoot = true;
                break;
            case InputAction.Reload:
                if (CanReload(ammo))
                    _toReload = true;
                break;
        }
    }

    public override WeaponStateType? NextStateType
    {
        get
        {
            if (_toShoot) return WeaponStateType.Shooting;
            if (_toReload) return WeaponStateType.Reloading;
            if (_toNoShoot) return WeaponStateType.NoShooting;
            return null;
        }
    }

    private static bool CanReload(AmmoComponent ammo) =>
        ammo.CurrentAmmo < ammo.MaxAmmo && !ammo.IsReloading && ammo.CurrentMagazine > 0;
}
