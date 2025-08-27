using Godot;

public partial class WeaponSystem : Node
{

    public ProjectileSystem ProjectileS { get; set; }

    private double _lastShotTime = 0;
    
    public override void _Ready()
    {
        ProjectileS = GetNode<ProjectileSystem>("/root/World/ProjectileSystem");
    }

    public void TryShoot(Node entity, WeaponComponent Weapon)
    {
        if (Weapon.Projectile == null)
            return;


        if (Weapon.Ammo == null)
            return;

        double currentTime = Time.GetTicksMsec() / 1000.0;
        if (currentTime - _lastShotTime < Weapon.FireRate.FireRateDelta || Weapon.Ammo.CurrentAmmo <= 0)
            return;
        

        ProjectileS.LinearTrayectory(entity, Weapon.Projectile);

        Weapon.Ammo.CurrentAmmo--;
        _lastShotTime = currentTime; 
    }


}