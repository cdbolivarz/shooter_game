public class PlayerSystems
{

    public WeaponSystem weaponSystem { get; private set; }

    public PlayerSystems()
    {
        weaponSystem = new WeaponSystem();
    }

    public void Update(float delta)
    {
        weaponSystem?.stateMachine?.Update(delta);
    }

    public void HandleInputAction(InputAction action)
    {
        weaponSystem?.stateMachine?.HandleAction(action);
    }


}