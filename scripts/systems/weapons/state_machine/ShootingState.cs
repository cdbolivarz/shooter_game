using Godot;

public class ShootingState : IWeaponState
{
    public void Enter(WeaponEntity weapon)
    {
        GD.Print($"{weapon.Name} started shooting");
        //weapon.ShootProjectile(); // tu lógica de disparo
    }

    public void Update(WeaponEntity weapon, double delta)
    {
        weapon.TryShoot();
    }

    public void Exit(WeaponEntity weapon)
    {
        GD.Print($"{weapon.Name} stopped shooting");
    }
}