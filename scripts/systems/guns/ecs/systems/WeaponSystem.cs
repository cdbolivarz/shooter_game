using Godot;
using System.Runtime.Serialization.Formatters;
using System.Threading.Tasks;

public partial class WeaponSystem : Node
{

    public ProjectileSystem ProjectileS { get; set; }

    private double _lastShotTime = 0;

    public override void _Ready()
    {
        ProjectileS = GetNode<ProjectileSystem>("/root/World/ProjectileSystem");
    }

    public void TryShoot(Marker2D Cannon, WeaponComponent Weapon)
    {
        if (Weapon.Projectile == null)
            return;


        if (Weapon.Ammo == null)
            return;

        if (Weapon.Ammo.IsReloading)
            return;

        double currentTime = Time.GetTicksMsec() / 1000.0;
        if (currentTime - _lastShotTime < Weapon.FireRate.FireRateDelta || Weapon.Ammo.CurrentAmmo <= 0)
            return;


        ProjectileS.Shoot(Cannon, Weapon.Projectile);

        if (Weapon.Ammo.MaxAmmo > 0)
        {
            Weapon.Ammo.CurrentAmmo--;
        }
        _lastShotTime = currentTime;
    }

    public async Task Reload(WeaponComponent Weapon)
    {
        if (
            Weapon.Ammo == null || Weapon.Ammo.MaxAmmo <= 0 ||
            Weapon.Ammo.CurrentMagazine == 0 || Weapon.Ammo.CurrentAmmo == Weapon.Ammo.MaxAmmo ||
            Weapon.Ammo.IsReloading
            )
            return;


        Weapon.Ammo.IsReloading = true;

        if (Weapon.Ammo.MaxMagazine > 0)
            Weapon.Ammo.CurrentMagazine--;

        GD.Print($"[WeaponSystem] Reloading... Current Ammo: {Weapon.Ammo.CurrentAmmo}, Current Magazine: {Weapon.Ammo.CurrentMagazine}");
        await ToSignal(GetTree().CreateTimer(Weapon.Ammo.ReloadTime), SceneTreeTimer.SignalName.Timeout);
        Weapon.Ammo.CurrentAmmo = Weapon.Ammo.MaxAmmo;
        GD.Print($"[WeaponSystem] Reloaded. Current Ammo: {Weapon.Ammo.CurrentAmmo}, Current Magazine: {Weapon.Ammo.CurrentMagazine}");
        Weapon.Ammo.IsReloading = false;
    }


}