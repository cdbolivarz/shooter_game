using Godot;

public partial class ProjectileSystem : Node
{

    public void RegisterProjectileSignals(ProjectileEntity projectile)
    {
        projectile.ProjectileHit += OnProjectileHit;
    }

    private void OnProjectileHit(Node target)
    {
        GD.Print($"[ProjectileSystem] Projectile hit {target.Name}");
    }

    public Node2D LoadProjectile(Marker2D Cannon, ProjectileComponent projectile)
    {
        if (projectile.ProjectileScene == null)
            return null;

        var projectile_scene = projectile.ProjectileScene.Instantiate<Node2D>();
        Cannon.GetTree().CurrentScene.AddChild(projectile_scene);

        projectile_scene.GlobalPosition = Cannon.GlobalPosition;
        RegisterProjectileSignals(projectile_scene as ProjectileEntity);

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