using Godot;

public interface IPlayerState
{
    void Enter();
    void Exit();
    void Update(float delta);
    
    void HandleMove(Vector2 inputDirection);
    void HandleJump();
    void HandleShoot();
    
    PlayerStateType GetNextStateType();
}
