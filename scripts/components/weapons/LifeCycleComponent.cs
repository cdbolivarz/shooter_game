using Godot;

public partial class LifeCycleComponent
{
    [Export] public float Duration { get; set; } = 0.0f; // Duration in seconds
    [Export] public float MaxCollitions { get; set; }
    [Export] public string OnExpireEffect { get; set; }
    [Export] public string OnCollideEffect { get; set; }
    // Here something can reproduce more lives? XD. Any component that has a life cycle could have a respawn?
    // And Something like metamorphosis or postmortem



}