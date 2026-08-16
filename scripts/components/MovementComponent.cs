using ShooterGame.Core;

namespace ShooterGame.Components;

/// <summary>Movement tuning data for a character. Pure data — logic lives in states/systems.</summary>
public class MovementComponent : IComponent
{
    public float MoveSpeed { get; set; } = 200f;
    public float AirMoveSpeed { get; set; } = 150f;
    public float JumpForce { get; set; } = -400f;
    public float Gravity { get; set; } = 980f;
    public float MaxFallSpeed { get; set; } = 800f;

    public MovementComponent() { }

    public MovementComponent(float moveSpeed, float airMoveSpeed, float jumpForce, float gravity, float maxFallSpeed)
    {
        MoveSpeed = moveSpeed;
        AirMoveSpeed = airMoveSpeed;
        JumpForce = jumpForce;
        Gravity = gravity;
        MaxFallSpeed = maxFallSpeed;
    }
}
