public class PlayerSystems : IDamagable, IWeapons, IPlatforms
{
    public WeaponSystem weaponSystem { get; set; }
    public PlatformSystem platformSystem { get; set; }
    public DamageSystem damageSystem { get; set; }

    public PlayerSystems(WeaponSystem weaponSystem = null, PlatformSystem platformSystem = null, DamageSystem damageSystem = null)
    {
        this.weaponSystem = weaponSystem ?? new WeaponSystem();
        this.platformSystem = platformSystem ?? new PlatformSystem();
        this.damageSystem = damageSystem;
    }

    public void Update(float delta)
    {
        weaponSystem?.stateMachine?.Update(delta);
        // Si PlatformSystem necesita update, agregar aquí
    }

    public void HandleInputAction(InputAction action)
    {
        weaponSystem?.stateMachine?.HandleAction(action);
        // Si PlatformSystem necesita input, agregar aquí
    }
}