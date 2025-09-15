using Godot;

public class ShootingState : WeaponStateBase
{
    private bool shouldTransitionToNoShoot = false;
    private bool shouldTransitionToReload = false;
    private bool shouldTransitionToSwitchWeapon = false;


    public ShootingState(WeaponEntity weapon) :
        base(weapon)
    { }

    public override void Enter()
    {
        shouldTransitionToNoShoot = false;
        shouldTransitionToReload = false;
        shouldTransitionToSwitchWeapon = false;
    }

    public override void Update(float delta)
    {
        WeaponSystem.TryShoot(weapon);
        if (weapon.Ammo.CurrentAmmo <= 0)
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
                if (weapon.Ammo.CurrentAmmo < weapon.Ammo.MaxAmmo && !weapon
                    .Ammo.IsReloading && weapon.Ammo.CurrentMagazine > 0)
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