using System;
using Godot;

public partial class ProjectileSystem
{

    public static void RegisterProjectileSignals(ProjectileEntity projectile_scene, ProjectileComponent projectile)
    {
        projectile_scene.Projectile = projectile;
        projectile_scene.ProjectileHit += OnProjectileHit;
    }

    private static void OnProjectileHit(Node projectile_scene, Node target)
    {
        var projectile_entity = projectile_scene as ProjectileEntity;
        
        if (target.HasMethod("TakeDamage"))
            target.CallDeferred("TakeDamage", projectile_entity.Projectile.Damage.CollitionDamage);

        projectile_entity.Projectile.CollitionsQuantity++;
        if (projectile_entity.Projectile.CollitionsQuantity >= projectile_entity.Projectile.LifeCycle.MaxCollitions)
            projectile_scene.QueueFree();

    }

    public static Node2D LoadProjectile(Marker2D Cannon, ProjectileComponent projectile)
    {
        if (projectile.ProjectileScene == null){
            return null;
        }

        var projectile_scene = projectile.ProjectileScene.Instantiate<Node2D>();
        projectile_scene.Name = "Bullet_" + Guid.NewGuid().ToString();
        // Get main scene to add the projectile to the root of the scene tree
        Cannon.AddChild(projectile_scene);

        projectile_scene.GlobalPosition = Cannon.GlobalPosition;
        ProjectileEntity projectile_entity = projectile_scene as ProjectileEntity;
        projectile_entity.ProjectileSprite.FlipH = Cannon.Position.X < 0;

        RegisterProjectileSignals(projectile_entity, projectile);

        return projectile_scene;
    }

    public static void Shoot(Marker2D Cannon, ProjectileComponent copy_from_projectile)
    {
        var projectile = new ProjectileComponent(copy_from_projectile);

        var projectile_scene = LoadProjectile(Cannon, projectile);

        switch (projectile.Mode)
        {
            case "Linear":
                LinearTrayectory(projectile_scene as RigidBody2D, projectile);
                break;
            // Future modes can be added here
            default:
                LinearTrayectory(projectile_scene as RigidBody2D, projectile);
                break;
        }


    }

    public static void LinearTrayectory(RigidBody2D projectile_scene, ProjectileComponent projectile)
    {
        projectile_scene.LinearVelocity = projectile.LinearSpeed;
    }

}