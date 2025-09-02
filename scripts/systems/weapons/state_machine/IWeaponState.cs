using Godot;

public interface IWeaponState
{
    void Enter(WeaponEntity weapon);
    void Update(WeaponEntity weapon, double delta);
    void Exit(WeaponEntity weapon);
}
