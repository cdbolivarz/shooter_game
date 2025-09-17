using Godot;

public class ShootingState : WeaponStateBase
{
    private bool shouldTransitionToNoShoot = false;
    private bool shouldTransitionToReload = false;
    private bool shouldTransitionToSwitchWeapon = false;


    public ShootingState(WeaponSystem weaponSystem) :
        base(weaponSystem)
    { }

    public override void Enter()
    {
        shouldTransitionToNoShoot = false;
        shouldTransitionToReload = false;
        shouldTransitionToSwitchWeapon = false;
    }

    public override void Update(float delta)
    {
        weaponSystem.TryShoot();
        if (weaponSystem.currentWeapon.Ammo.CurrentAmmo <= 0)
        {
            shouldTransitionToNoShoot = true;
        }
    }

    public override void Exit()
    {
    }

    public override WeaponStateType GetNextStateType()
    {
        if (shouldTransitionToNoShoot)
        {
            return WeaponStateType.NoShooting;
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
            case InputAction.Reload:
                if (weaponSystem.currentWeapon.Ammo.CurrentAmmo < weaponSystem.currentWeapon.Ammo.MaxAmmo && !weaponSystem.currentWeapon
                    .Ammo.IsReloading && weaponSystem.currentWeapon.Ammo.CurrentMagazine > 0)
                {
                    shouldTransitionToReload = true;
                }
                break;
            case InputAction.SwitchWeapon:
                shouldTransitionToSwitchWeapon = true;
                break;
            case InputAction.ShootReleased:
                shouldTransitionToNoShoot = true;
                break;
        }
    }
}