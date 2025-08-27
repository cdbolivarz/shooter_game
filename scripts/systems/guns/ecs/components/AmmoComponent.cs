using Godot;

public partial class AmmoComponent : Node
{
    [Export] public float MaxAmmo { get; set; } = 30f; // -1 means infinite ammo
    [Export] public float ReloadTime { get; set; } = 1.5f;
    [Export] public int MaxMagazine { get; set; } = 3; // -1 means infinite magazines

    public bool IsReloading { get; set; } = false;
    public float CurrentAmmo { get; set; }
    public float CurrentMagazine { get; set; }

    public override void _Ready()
    {
        CurrentAmmo = MaxAmmo;
        CurrentMagazine = MaxMagazine;
    }
}