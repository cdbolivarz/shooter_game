using Godot;


public class ReloadingState : IWeaponState
{
    private double _reloadTimer;

    public void Enter(WeaponEntity weapon)
    {
        GD.Print($"{weapon.Name} started reloading");
        _reloadTimer = weapon.Ammo.ReloadTime;
    }

    public void Update(WeaponEntity weapon, double delta)
    {
        _reloadTimer -= delta;
        if (_reloadTimer <= 0)
        {
            weapon.Reload();
            weapon.ChangeState(new NoShootingState());
        }
    }

    public void Exit(WeaponEntity weapon)
    {
        GD.Print($"{weapon.Name} finished reloading");
    }
}