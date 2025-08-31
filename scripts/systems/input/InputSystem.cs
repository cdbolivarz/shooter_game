using Godot;

public static class InputSystem
{
    public static event System.Action<InputAction> OnActionTriggered;
    public static event System.Action<InputAction> OnActionReleased;
    
    public static void ProcessInput()
    {
        if (Input.IsActionJustPressed("move_left"))
            OnActionTriggered?.Invoke(InputAction.MoveLeft);
        if (Input.IsActionJustPressed("move_right"))
            OnActionTriggered?.Invoke(InputAction.MoveRight);
        if (Input.IsActionJustPressed("jump"))
            OnActionTriggered?.Invoke(InputAction.Jump);
        if (Input.IsActionJustPressed("shoot"))
            OnActionTriggered?.Invoke(InputAction.Shoot);
        
        if (Input.IsActionJustReleased("move_left"))
            OnActionReleased?.Invoke(InputAction.MoveLeft);
        if (Input.IsActionJustReleased("move_right"))
            OnActionReleased?.Invoke(InputAction.MoveRight);
        if (Input.IsActionJustReleased("shoot"))
            OnActionReleased?.Invoke(InputAction.Shoot);
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
