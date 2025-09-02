using System;
using Godot;

public partial class ProjectileSystem : Node
{

    public void RegisterProjectileSignals(ProjectileEntity projectile_scene, ProjectileComponent projectile)
    {
        projectile_scene.Projectile = projectile;
        projectile_scene.ProjectileHit += OnProjectileHit;
    }

    private void OnProjectileHit(Node projectile_scene, Node target)
    {
        var projectile_entity = projectile_scene as ProjectileEntity;
        projectile_entity.Projectile.CollitionsQuantity++;
        if (projectile_entity.Projectile.CollitionsQuantity >= projectile_entity.Projectile.LifeCycle.MaxCollitions)
            projectile_scene.QueueFree();

    }

    public Node2D LoadProjectile(Marker2D Cannon, ProjectileComponent projectile)
    {
        if (projectile.ProjectileScene == null)
            return null;

        var projectile_scene = projectile.ProjectileScene.Instantiate<Node2D>();
        projectile_scene.Name = "Bullet_" + Guid.NewGuid().ToString();
        // Get main scene to add the projectile to the root of the scene tree
        GetTree().Root.GetChild(0).AddChild(projectile_scene);

        projectile_scene.GlobalPosition = Cannon.GlobalPosition;

        var new_projectile = GetNode<ProjectileEntity>(projectile_scene.GetPath());
        RegisterProjectileSignals(new_projectile, projectile);

        return projectile_scene;
    }

    public void Shoot(Marker2D Cannon, ProjectileComponent projectile)
    {

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

    public void LinearTrayectory(RigidBody2D projectile_scene, ProjectileComponent projectile)
    {
        projectile_scene.LinearVelocity = projectile.LinearSpeed;
    }

}