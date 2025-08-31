using Godot;

public static class InputSystem
{
    public static event System.Action<InputAction> OnActionTriggered;
    public static event System.Action<InputAction> OnActionReleased;
    
    public static void ProcessInput()
    {
        if (Input.IsActionJustPressed("jump"))
            OnActionTriggered?.Invoke(InputAction.Jump);
        if (Input.IsActionJustPressed("shoot"))
            OnActionTriggered?.Invoke(InputAction.Shoot);
    }
    
    public static Vector2 GetMovementInput()
    {
        Vector2 input = Vector2.Zero;
        if (Input.IsActionPressed("move_left"))
            input.X -= 1;
        if (Input.IsActionPressed("move_right"))
            input.X += 1;
        return input;
    }
}
