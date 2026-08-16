using ShooterGame.Core;
using ShooterGame.Input;
using ShooterGame.Systems;

namespace ShooterGame.States.Weapon;

public abstract class WeaponStateBase : IWeaponState
{
    protected readonly WeaponSystem WeaponSystem;

    protected WeaponStateBase(WeaponSystem weaponSystem)
    {
        WeaponSystem = weaponSystem;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update(float delta) { }
    public virtual void HandleAction(InputAction action) { }
    public virtual WeaponStateType? NextStateType => null;
}
