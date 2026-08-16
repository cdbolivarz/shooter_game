using Godot;
using ShooterGame.Components;
using ShooterGame.Core;

namespace ShooterGame.Entities;

public partial class ProjectileEntity : RigidBody2D, IEntity
{
    [Signal] public delegate void ProjectileHitEventHandler(Node2D projectileScene, Node target);

    public ComponentBag Components { get; } = new();

    /// <summary>Per-projectile data, added by <see cref="ProjectileSystem"/> at fire time.</summary>
    public ProjectileComponent Projectile => Components.Get<ProjectileComponent>();

    [Export] public Sprite2D ProjectileSprite { get; set; }

    private void OnBodyEntered(Node body)
    {
        EmitSignal(SignalName.ProjectileHit, this, body);
    }

    private void OnScreenExited()
    {
        QueueFree();
    }
}
