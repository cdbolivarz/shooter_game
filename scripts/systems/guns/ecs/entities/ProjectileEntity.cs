using Godot;


public partial class ProjectileEntity : RigidBody2D
{
    [Signal] public delegate void ProjectileHitEventHandler(Node target);

    private void OnBodyEntered(Node body)
    {
        EmitSignal(SignalName.ProjectileHit, body);
    }

    private void OnScreenExited()
    {
        QueueFree();
    }
}