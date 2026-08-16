using Godot;
using ShooterGame.Animations;
using ShooterGame.Entities;
using ShooterGame.Input;

namespace ShooterGame.States.Player;

public class GroundState : PlayerStateBase
{
    private bool _toAirborne;

    public GroundState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        _toAirborne = false;
        AnimationPlayer.Play(PlayerAnimationEnum.Idle.ToAnimationName());
    }

    public override void Update(float delta)
    {
        if (!Player.IsOnFloor())
            _toAirborne = true;
    }

    public override void HandleInputAction(InputAction action)
    {
        switch (action)
        {
            case InputAction.Jump:
                Vector2 velocity = Player.Velocity;
                velocity.Y = Movement.JumpForce;
                Player.Velocity = velocity;
                AnimationPlayer.Play(PlayerAnimationEnum.Jump.ToAnimationName());
                _toAirborne = true;
                break;

            case InputAction.EquipWeapon:
                EquipWeaponIfEmpty();
                break;

            case InputAction.DownPlatform:
                Player.PlatformSystem?.DropThroughPlatform(Player);
                break;

            default:
                ForwardWeaponInput(action);
                break;
        }
    }

    public override void HandleMovement(Vector2 inputDirection)
    {
        Vector2 velocity = Player.Velocity;
        velocity.X = inputDirection.X * Movement.MoveSpeed;
        Player.Velocity = velocity;

        AnimationPlayer.Play(inputDirection.X != 0
            ? PlayerAnimationEnum.Walk.ToAnimationName()
            : PlayerAnimationEnum.Idle.ToAnimationName());
    }

    public override PlayerStateType? NextStateType => _toAirborne ? PlayerStateType.Airborne : null;
}
