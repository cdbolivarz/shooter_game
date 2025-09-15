

public class NoShootingState : WeaponStateBase
{
    private bool shouldTransitionToShoot = false;
    private bool shouldTransitionToReload = false;
    private bool shouldTransitionToSwitchWeapon = false;

    public NoShootingState(WeaponEntity weapon) :
        base(weapon)
    { }

    public override void Enter()
    {
        shouldTransitionToShoot = false;
        shouldTransitionToReload = false;
        shouldTransitionToReload = false;
    }

    public override void Update(float delta) { }
    public override void Exit() { }

    public override WeaponStateType GetNextStateType()
    {
        if (shouldTransitionToShoot)
        {
            return WeaponStateType.Shooting;
        }
        if (shouldTransitionToReload)
        {
            return WeaponStateType.Reloading;
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
                weapon.Ammo.IsReloading = false;
                shouldTransitionToShoot = true;
                break;
            case InputAction.Reload:
                if (weapon.Ammo.CurrentAmmo < weapon.Ammo.MaxAmmo && !weapon
                    .Ammo.IsReloading && weapon.Ammo.CurrentMagazine > 0)
                {
                    shouldTransitionToReload = true;
                }
                break;
            case InputAction.SwitchWeapon:
                // maybe evaluate if can switch weapon
                shouldTransitionToSwitchWeapon = true;
                break;
        }
    }
}
