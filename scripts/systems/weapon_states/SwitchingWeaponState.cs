using Godot;

public class SwitchingWeaponState : WeaponStateBase
{

    private double _switchTimer = 0.5; // Time it takes to switch weapon
    private bool shouldTransitionToShoot = false;
    private bool shouldTransitionToReload = false;
    private bool shouldTransitionToNoShoot = false;
    public SwitchingWeaponState(WeaponSystem weaponSystem) :
        base(weaponSystem)
    { }

    public override void Enter()
    {
        shouldTransitionToShoot = false;
        shouldTransitionToReload = false;
        shouldTransitionToNoShoot = false;
    }

    public override void Update(float delta)
    {
        _switchTimer -= delta;
        if (_switchTimer <= 0)
        {
            weaponSystem.EquipWeapon(weaponSystem.currentWeapon.GetParent<Node2D>());
            shouldTransitionToNoShoot = true;
        }
    }

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
        if (shouldTransitionToNoShoot)
        {
            return WeaponStateType.NoShooting;
        }
        return WeaponStateType.None;
    }

    public override void HandleAction(InputAction action)
    {
        switch (action)
        {
            case InputAction.Shoot:
                shouldTransitionToShoot = true;
                break;
            case InputAction.Reload:
                if (weaponSystem.currentWeapon.Ammo.CurrentAmmo < weaponSystem.currentWeapon.Ammo.MaxAmmo && !weaponSystem.currentWeapon
                    .Ammo.IsReloading && weaponSystem.currentWeapon.Ammo.CurrentMagazine > 0)
                {
                    shouldTransitionToReload = true;
                }
                break;
        }
    }
    
}