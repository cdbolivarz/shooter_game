using Godot;

public class WeaponStateFactory
{
    private WeaponSystem weaponSystem;
    
    public WeaponStateFactory(WeaponSystem weaponSystem)
    {
        this.weaponSystem = weaponSystem;
    }

    
    public IWeaponState CreateState(WeaponStateType stateType)
    {
        return stateType switch
        {
            WeaponStateType.Shooting => new ShootingState(weaponSystem),
            WeaponStateType.NoShooting => new NoShootingState(weaponSystem),
            WeaponStateType.SwitchingWeapon => new SwitchingWeaponState(weaponSystem),
            WeaponStateType.Reloading => new ReloadingState(weaponSystem),
            _ => null
        };
    }
}
