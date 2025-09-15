using Godot;

public class WeaponStateFactory
{
    private WeaponEntity weapon;
    
    public WeaponStateFactory(WeaponEntity weapon)
    {
        this.weapon = weapon;
    }

    
    public IWeaponState CreateState(WeaponStateType stateType)
    {
        return stateType switch
        {
            WeaponStateType.Shooting => new ShootingState(weapon),
            WeaponStateType.NoShooting => new NoShootingState(weapon),
            WeaponStateType.SwitchingWeapon => new SwitchingWeaponState(weapon),
            WeaponStateType.Reloading => new ReloadingState(weapon),
            _ => null
        };
    }
}
