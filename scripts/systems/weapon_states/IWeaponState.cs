public interface IWeaponState
{
    void Enter();
    void Update(float delta);
    void Exit();

    WeaponStateType GetNextStateType();
    void HandleAction(InputAction action);

}
