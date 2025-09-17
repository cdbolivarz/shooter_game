using Godot;

public abstract class WeaponStateBase : IWeaponState
{
    protected WeaponSystem weaponSystem;
    
    protected WeaponStateBase(WeaponSystem weaponSystem)
    {
        this.weaponSystem = weaponSystem;
    }
    
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update(float delta) { }

    public virtual void HandleAction(InputAction action) { }
    
    public virtual WeaponStateType GetNextStateType() => WeaponStateType.None;
}
