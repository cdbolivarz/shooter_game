using ShooterGame.Core;
using ShooterGame.Input;

namespace ShooterGame.States.Weapon;

public interface IWeaponState : IState<WeaponStateType>
{
    void HandleAction(InputAction action);
}
