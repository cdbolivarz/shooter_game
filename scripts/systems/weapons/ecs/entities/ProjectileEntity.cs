using Godot;


public partial class ProjectileEntity : RigidBody2D
{
    [Signal] public delegate void ProjectileHitEventHandler(Node2D projectil_scene, Node target);
    [Export] public Sprite2D ProjectileSprite { get; set; }

    public ProjectileComponent Projectile { get; set; }


    private void OnBodyEntered(Node body)
    {
        EmitSignal(SignalName.ProjectileHit, this, body);
    }

    private void OnScreenExited()
    {
        QueueFree();
    }
}