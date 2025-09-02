using Godot;
using System;

public partial class WeaponEntity : Node2D
{
    public ProjectileComponent Projectile { get; set; } = new ProjectileComponent();
    public FireRateComponent FireRate { get; set; } = new FireRateComponent();
    public AmmoComponent Ammo { get; set; } = new AmmoComponent();
    [Export] public Marker2D Cannon { get; set; }
    private double _lastShotTime = 0;
    public ProjectileSystem ProjectileS { get; set; }


    private IWeaponState _currentState;

    public override void _Ready()
    {
        ProjectileS = GetNode<ProjectileSystem>("/root/World/ProjectileSystem");
        ChangeState(new NoShootingState());
    }

    public override void _Process(double delta)
    {
        _currentState?.Update(this, delta);
    }

    public void ChangeState(IWeaponState newState)
    {
        _currentState?.Exit(this);
        _currentState = newState;
        _currentState.Enter(this);
    }


    public void Reload()
    {

        if (
        Ammo == null || Ammo.MaxAmmo <= 0 ||
        Ammo.CurrentMagazine == 0 || Ammo.CurrentAmmo == Ammo.MaxAmmo ||
        Ammo.IsReloading
        )
            return;


        Ammo.IsReloading = true;


        GD.Print($"[WeaponSystem] Reloading... Current Ammo: {Ammo.CurrentAmmo}, Current Magazine: {Ammo.CurrentMagazine}");
        float ammo_to_reload = (float)Math.Min(Ammo.MaxAmmo - Ammo.CurrentAmmo, Ammo.MaxAmmo);
        if (Ammo.CurrentMagazine >= 0)
            Ammo.CurrentMagazine -= ammo_to_reload;
        Ammo.CurrentAmmo += ammo_to_reload;
        GD.Print("Reload complete!");
        Ammo.IsReloading = false;

    }

    public void TryShoot()
    {
        
        if (Projectile == null)
            return;


        if (Ammo == null)
            return;

        Ammo.IsReloading = false; // Cancel reload if shooting

        double currentTime = Time.GetTicksMsec() / 1000.0;
        if (currentTime - _lastShotTime < FireRate.FireRateDelta || Ammo.CurrentAmmo <= 0)
            return;
        
        ProjectileS.Shoot(Cannon, Projectile);

        if (Ammo.MaxAmmo > 0)
        {
            Ammo.CurrentAmmo--;
        }
        _lastShotTime = currentTime;
    }
}