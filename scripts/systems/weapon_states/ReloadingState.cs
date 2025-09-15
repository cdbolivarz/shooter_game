using Godot;


public class ReloadingState : WeaponStateBase
{
    private double _reloadTimer;
    private bool shouldTransitionToNoShooting = false;
    private bool shouldTransitionToShoot = false;
    private bool shouldTransitionToSwitchWeapon = false;



    public ReloadingState(WeaponEntity weapon) :
        base(weapon)
    { }

    public override void Enter()
    {
        shouldTransitionToNoShooting = false;
        shouldTransitionToShoot = false;
        shouldTransitionToSwitchWeapon = false;

        _reloadTimer = weapon.Ammo.ReloadTime;
    }

    public override void Update(float delta)
    {
        _reloadTimer -= delta;
        if (_reloadTimer <= 0)
        {
            WeaponSystem.Reload(weapon);
            shouldTransitionToNoShooting = true;
        }
    }

    public void Exit(WeaponEntity weapon)
    {
        weapon.Ammo.IsReloading = false;
    }

    public override WeaponStateType GetNextStateType()
    {
        if (shouldTransitionToNoShooting)
        {
            return WeaponStateType.NoShooting;
        }
        if (shouldTransitionToShoot)
        {
            return WeaponStateType.Shooting;
        }
        if (shouldTransitionToSwitchWeapon)
        {
            return WeaponStateType.SwitchingWeapon;
        }
        return WeaponStateType.None;
    }

    public override void HandleAction(InputAction action)
    {
        switch (action)
        {
            case InputAction.Shoot:
                if (weapon.Ammo.CurrentAmmo > 0)
                {
                    shouldTransitionToShoot = true;
                }
                break;
            case InputAction.SwitchWeapon:
                // maybe evaluate if can switch weapon
                shouldTransitionToSwitchWeapon = true;
                break;
        }
    }
}
