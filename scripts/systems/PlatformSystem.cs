using Godot;

namespace ShooterGame.Systems;

public class PlatformSystem
{
    public int CurrentPlatformLayer { get; set; } = 1;

    public async void DropThroughPlatform(CharacterBody2D body)
    {
        body.SetCollisionMaskValue(1, false);
        await body.ToSignal(body.GetTree().CreateTimer(0.2), "timeout");
        body.SetCollisionMaskValue(1, true);
    }
}
