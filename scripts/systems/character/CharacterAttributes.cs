using Godot;

public class CharacterAttributes
{
    [Export] public float moveSpeed = 200.0f;
    [Export] public float airMoveSpeed = 150.0f;
    [Export] public float jumpForce = -400.0f;
    [Export] public float gravity = 980.0f;
    [Export] public float maxFallSpeed = 800.0f;
    
    public CharacterAttributes() { }
    
    public CharacterAttributes(float moveSpeed, float airMoveSpeed, float jumpForce, float gravity, float maxFallSpeed)
    {
        this.moveSpeed = moveSpeed;
        this.airMoveSpeed = airMoveSpeed;
        this.jumpForce = jumpForce;
        this.gravity = gravity;
        this.maxFallSpeed = maxFallSpeed;
    }
}
