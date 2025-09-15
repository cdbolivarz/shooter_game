using Godot;

public class SwitchingWeaponState : WeaponStateBase
{

    private bool shouldTransitionToShoot = false;
    private bool shouldTransitionToReload = false;
    private bool shouldTransitionToNoShoot = false;
    public SwitchingWeaponState(WeaponEntity weapon) :
        base(weapon)
    { }

    public override void Enter()
    {
        shouldTransitionToShoot = false;
        shouldTransitionToReload = false;
        shouldTransitionToNoShoot = false;
    }

    public override void Update(float delta)
    {
        // lógica para finalizar switch
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
        // return WeaponStateType.None; always switch to NoShooting (until we implement a switch logic)
        return WeaponStateType.NoShooting;
    }
    
}