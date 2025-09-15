using Godot;

public abstract class WeaponStateBase : IWeaponState
{
    protected WeaponEntity weapon;
    
    protected WeaponStateBase(WeaponEntity weapon)
    {
        this.weapon = weapon;
    }
    
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update(float delta) { }

    public virtual void HandleAction(InputAction action) { }
    
    public virtual WeaponStateType GetNextStateType() => WeaponStateType.None;
}
