using Godot;

public partial class ProjectileSystem : Node
{

    public void LinearTrayectory(Node entity, ProjectileComponent projectile)
    // For now this method is a power trayectory
    {
        if (projectile.ProjectileScene == null)
            return;

        var projectile_scene = projectile.ProjectileScene.Instantiate<Node2D>();
        entity.GetTree().CurrentScene.AddChild(projectile_scene);

        projectile_scene.Position += ((Node2D)entity).Position;

        if (projectile_scene is RigidBody2D rb)
        {
            rb.LinearVelocity = projectile.LinearSpeed; // Assuming rightward shooting
        // rb.ApplyCentralImpulse(new Vector2(weapon.ProjectileSpeed, 0)); // Apply an impulse for the projectile speed
        } 
    }

}