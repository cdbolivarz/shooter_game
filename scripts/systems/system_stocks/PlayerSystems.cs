public class PlayerSystems
{
    public WeaponSystem weaponSystem { get; private set; }
    public PlatformSystem platformSystem { get; private set; }

    public PlayerSystems()
    {
        weaponSystem = new WeaponSystem();
        platformSystem = new PlatformSystem();
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