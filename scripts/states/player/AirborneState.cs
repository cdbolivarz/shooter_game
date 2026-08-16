using Godot;
using ShooterGame.Animations;
using ShooterGame.Entities;
using ShooterGame.Input;

namespace ShooterGame.States.Player;

public class AirborneState : PlayerStateBase
{
    private bool _toGround;

    public AirborneState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        _toGround = false;
        AnimationPlayer.Play(Player.Velocity.Y < 0
            ? PlayerAnimationEnum.Jump.ToAnimationName()
            : PlayerAnimationEnum.Fall.ToAnimationName());
    }

    public override void Update(float delta)
    {
        Vector2 velocity = Player.Velocity;
        velocity.Y += Movement.Gravity * delta;
        velocity.Y = Mathf.Min(velocity.Y, Movement.MaxFallSpeed);
        Player.Velocity = velocity;

        if (Player.IsOnFloor())
        {
            _toGround = true;
        }
        else if (velocity.Y > 0 && AnimationPlayer.CurrentAnimation != PlayerAnimationEnum.Fall.ToAnimationName())
        {
            AnimationPlayer.Play(PlayerAnimationEnum.Fall.ToAnimationName());
        }
    }

    public override void HandleInputAction(InputAction action)
    {
        switch (action)
        {
            case InputAction.Jump:
                if (Player.Velocity.Y < 0)
                {
                    Vector2 velocity = Player.Velocity;
                    velocity.Y = Mathf.Max(velocity.Y, -200f);
                    Player.Velocity = velocity;
                }
                break;

            case InputAction.EquipWeapon:
                EquipWeaponIfEmpty();
                break;

            default:
                ForwardWeaponInput(action);
                break;
        }
    }

    public override void HandleMovement(Vector2 inputDirection)
    {
        Vector2 velocity = Player.Velocity;
        velocity.X = inputDirection.X * Movement.AirMoveSpeed;
        Player.Velocity = velocity;
    }

    public override PlayerStateType? NextStateType => _toGround ? PlayerStateType.Ground : null;
}
