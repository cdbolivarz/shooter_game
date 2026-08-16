using Godot;

namespace ShooterGame.Input;

/// <summary>
/// Polls the global input map and raises C# events. Entities subscribe in
/// <c>_Ready</c> and unsubscribe in <c>_ExitTree</c> — no Godot signals used.
/// Note: use <c>Godot.Input</c> explicitly here — the enclosing namespace
/// is also named <c>Input</c> and would otherwise shadow the type.
/// </summary>
public static class InputSystem
{
    public static event System.Action<InputAction> OnActionTriggered;

    public static void ProcessInput()
    {
        if (Godot.Input.IsActionPressed("bend") && Godot.Input.IsActionJustPressed("jump"))
            OnActionTriggered?.Invoke(InputAction.DownPlatform);
        else if (Godot.Input.IsActionJustPressed("jump"))
            OnActionTriggered?.Invoke(InputAction.Jump);

        if (Godot.Input.IsActionJustPressed("shoot"))
            OnActionTriggered?.Invoke(InputAction.Shoot);
        if (Godot.Input.IsActionJustPressed("reload"))
            OnActionTriggered?.Invoke(InputAction.Reload);
        if (Godot.Input.IsActionJustPressed("switch_weapon"))
            OnActionTriggered?.Invoke(InputAction.SwitchWeapon);
        if (Godot.Input.IsActionJustPressed("equip_weapon"))
            OnActionTriggered?.Invoke(InputAction.EquipWeapon);
        if (Godot.Input.IsActionJustReleased("shoot"))
            OnActionTriggered?.Invoke(InputAction.ShootReleased);
    }

    public static Vector2 GetMovementInput()
    {
        Vector2 input = Vector2.Zero;
        if (Godot.Input.IsActionPressed("move_left"))
            input.X -= 1;
        if (Godot.Input.IsActionPressed("move_right"))
            input.X += 1;
        return input;
    }
}
