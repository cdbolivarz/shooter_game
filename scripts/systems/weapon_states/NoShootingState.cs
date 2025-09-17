

public class NoShootingState : WeaponStateBase
{
    private bool shouldTransitionToShoot = false;
    private bool shouldTransitionToReload = false;
    private bool shouldTransitionToSwitchWeapon = false;

    public NoShootingState(WeaponSystem weaponSystem) :
        base(weaponSystem)
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
                weaponSystem.currentWeapon.Ammo.IsReloading = false;
                shouldTransitionToShoot = true;
                break;
            case InputAction.Reload:
                if (weaponSystem.currentWeapon.Ammo.CurrentAmmo < weaponSystem.currentWeapon.Ammo.MaxAmmo && !weaponSystem.currentWeapon
                    .Ammo.IsReloading && weaponSystem.currentWeapon.Ammo.CurrentMagazine > 0)
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
