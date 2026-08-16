using Godot;
using ShooterGame.Components;
using ShooterGame.Entities;

namespace ShooterGame.Systems;

/// <summary>
/// Spawns projectiles from a weapon's muzzle. Projectiles are self-driving
/// RigidBody2D nodes (velocity + signals), so there is no per-frame loop here
/// yet; convert to an instance <see cref="ISystem"/> if homing/expiry/pooling
/// behavior is added later.
/// </summary>
public static class ProjectileSystem
{
    public static Node2D Shoot(Marker2D cannon, ProjectileComponent template)
    {
        if (cannon == null || template?.ProjectileScene == null)
            return null;

        var projectile = new ProjectileComponent(template);
        var scene = projectile.ProjectileScene.Instantiate<ProjectileEntity>();
        scene.Name = $"Bullet_{System.Guid.NewGuid()}";

        cannon.AddChild(scene);
        scene.GlobalPosition = cannon.GlobalPosition;

        scene.Components.Add(projectile);
        scene.ProjectileHit += OnProjectileHit;

        scene.ProjectileSprite.FlipH = cannon.Position.X < 0;

        switch (projectile.Mode)
        {
            case ProjectileMode.Linear:
            default:
                scene.LinearVelocity = projectile.LinearSpeed;
                break;
        }

        return scene;
    }

    private static void OnProjectileHit(Node2D projectileScene, Node target)
    {
        if (projectileScene is not ProjectileEntity projectile)
            return;

        if (target is IDamagable damagable)
            DamageSystem.ApplyDamage(damagable.Health, projectile.Projectile.Damage);

        projectile.Projectile.CollitionsQuantity++;
        if (projectile.Projectile.CollitionsQuantity >= projectile.Projectile.LifeCycle.MaxCollitions)
            projectile.QueueFree();
    }
}
