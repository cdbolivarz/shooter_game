using Godot;

public partial class ProjectileComponent : Node
{
    [Export] public PackedScene ProjectileScene { get; set; }
    public DamageComponent Damage { get; set; } = new DamageComponent();
    public LifeCycleComponent LifeCycle { get; set; } = new LifeCycleComponent();
    public Vector2 LinearSpeed { get; set; }
    public string Mode { get; set; } = "Linear"; // Default is linear, could be "Homing", "Arc", etc.
    public bool HasCollided { get; set; } = false;
    public int CollitionsQuantity { get; set; } = 0;

    public ProjectileComponent()
    {
    }

    public ProjectileComponent(ProjectileComponent copyFrom)
    {
        ProjectileScene = copyFrom.ProjectileScene;
        Damage = copyFrom.Damage;
        LifeCycle = copyFrom.LifeCycle;
        LinearSpeed = copyFrom.LinearSpeed;
        Mode = copyFrom.Mode;
    }
}