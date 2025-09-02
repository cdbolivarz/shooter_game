using Godot;

public class SwitchingWeaponState : IWeaponState
{
    public void Enter(WeaponEntity weapon)
    {
        GD.Print($"{weapon.Name} switching...");
        // podrías iniciar animación aquí
    }

    public void Update(WeaponEntity weapon, double delta)
    {
        // lógica para finalizar switch
        weapon.ChangeState(new NoShootingState());
    }

    public void Exit(WeaponEntity weapon) { }
}