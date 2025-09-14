using Godot;

public interface IPlayerState
{
    void Enter();
    void Exit();
    void Update(float delta);
    
    void HandleInputAction(InputAction action);
    void HandleMovement(Vector2 inputDirection);
    
    PlayerStateType GetNextStateType();
}
