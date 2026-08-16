using Godot;
using ShooterGame.Core;

namespace ShooterGame.Components;

public class ProjectileComponent : IComponent
{
    public PackedScene ProjectileScene { get; set; }
    public DamageComponent Damage { get; set; } = new();
    public LifeCycleComponent LifeCycle { get; set; } = new();
    public Vector2 LinearSpeed { get; set; }
    public ProjectileMode Mode { get; set; } = ProjectileMode.Linear;
    public bool HasCollided { get; set; }
    public int CollitionsQuantity { get; set; }

    public ProjectileComponent() { }

    /// <summary>Deep-copies the sub-components so each fired projectile owns its own state.</summary>
    public ProjectileComponent(ProjectileComponent copyFrom)
    {
        ProjectileScene = copyFrom.ProjectileScene;
        Damage = new DamageComponent(copyFrom.Damage);
        LifeCycle = new LifeCycleComponent(copyFrom.LifeCycle);
        LinearSpeed = copyFrom.LinearSpeed;
        Mode = copyFrom.Mode;
    }
}
