using Godot;

public partial class AmmoComponent
{
    [Export] public float MaxAmmo { get; set; } = 30f; // -1 means infinite ammo
    [Export] public float ReloadTime { get; set; } = 1.5f;
    [Export] public float MaxMagazine { get; set; } = 100f; // -1 means infinite magazine

    public bool IsReloading { get; set; } = false;
    public float CurrentAmmo { get; set; }
    public float CurrentMagazine { get; set; }

    public AmmoComponent()
    {
        CurrentAmmo = MaxAmmo;
        CurrentMagazine = MaxMagazine;
    }
}