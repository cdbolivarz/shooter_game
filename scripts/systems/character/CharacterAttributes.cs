using Godot;

public class CharacterAttributes
{
    [Export] public float moveSpeed { get; set; } = 200.0f;
    [Export] public float airMoveSpeed { get; set; } = 150.0f;
    [Export] public float jumpForce { get; set; } = -400.0f;
    [Export] public float gravity { get; set; } = 980.0f;
    [Export] public float maxFallSpeed { get; set; } = 800.0f;
    public HealthComponent healthComponent { get; set; }
    
    public CharacterAttributes() { }

    public CharacterAttributes(float moveSpeed, float airMoveSpeed, float jumpForce, float gravity, float maxFallSpeed, HealthComponent healthComponent = null)
    {
        this.moveSpeed = moveSpeed;
        this.airMoveSpeed = airMoveSpeed;
        this.jumpForce = jumpForce;
        this.gravity = gravity;
        this.maxFallSpeed = maxFallSpeed;
        this.healthComponent = healthComponent;
    }
}
