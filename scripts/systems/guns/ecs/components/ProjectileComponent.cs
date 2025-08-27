using Godot;

public partial class ProjectileComponent : Node
{
    [Export] public PackedScene ProjectileScene { get; set; }
    [Export] public DamageComponent Damage { get; set; }
    [Export] public LifeCycleComponent LifeCycle { get; set; }
    [Export] public Vector2 LinearSpeed { get; set; }
    [Export] public string Mode { get; set; } = "Linear"; // Default is linear, could be "Homing", "Arc", etc.
    public bool HasCollided { get; set; } = false;
    public int CollitionsQuantity { get; set; } = 0;

}