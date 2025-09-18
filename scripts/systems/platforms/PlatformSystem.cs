using Godot;

public class PlatformSystem
{
    public int currentPlataformLayer { get; set; } = 1; // Capa de las plataformas (bit 0)
    public PlatformSystem() { }

    public PlatformSystem(int currentPlataformLayer)
    {
        this.currentPlataformLayer = currentPlataformLayer;
    }

    public async void DropThroughPlatform(CharacterBody2D player)
    {
        player.SetCollisionMaskValue(1, false);
        await player.ToSignal(player.GetTree().CreateTimer(0.2), "timeout");
        player.SetCollisionMaskValue(1, true);
    }
}
