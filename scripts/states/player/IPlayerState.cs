using Godot;
using ShooterGame.Core;
using ShooterGame.Input;

namespace ShooterGame.States.Player;

public interface IPlayerState : IState<PlayerStateType>
{
    void HandleInputAction(InputAction action);
    void HandleMovement(Vector2 inputDirection);
}
