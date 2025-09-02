using Godot;

public partial class DamageComponent : Node
{
    [Export] public float DamagePerSecond { get; set; } 
    [Export] public bool IsAreaEffect { get; set; } = false;

    // More shadows? Mera traba xD
    [Export] public float AreaRadius { get; set; } = 0.0f; 
    [Export] public float CollitionDamage { get; set; } = 0.0f; 


}