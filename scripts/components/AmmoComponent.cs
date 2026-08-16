using ShooterGame.Core;

namespace ShooterGame.Components;

public class AmmoComponent : IComponent
{
    public float MaxAmmo { get; set; } = 30f;        // -1 = infinite ammo
    public float ReloadTime { get; set; } = 1.5f;
    public float MaxMagazine { get; set; } = 100f;   // -1 = infinite magazine

    public bool IsReloading { get; set; }
    public float CurrentAmmo { get; set; }
    public float CurrentMagazine { get; set; }

    public AmmoComponent()
    {
        CurrentAmmo = MaxAmmo;
        CurrentMagazine = MaxMagazine;
    }

    /// <summary>Re-syncs current ammo/magazine to their max (call after changing max values).</summary>
    public void ResetToFull()
    {
        CurrentAmmo = MaxAmmo;
        CurrentMagazine = MaxMagazine;
        IsReloading = false;
    }
}
