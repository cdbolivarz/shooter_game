using Godot;

public partial class WeaponSystem : Node
{

    public ProjectileSystem ProjectileS { get; set; }

    private double _lastShotTime = 0;

    public override void _Ready()
    {
        ProjectileS = GetNode<ProjectileSystem>("/root/World/ProjectileSystem");
    }
 public void HandleInput(WeaponEntity weapon)
    {
        if (Input.IsActionJustPressed("shoot"))
        {
            weapon.ChangeState(new ShootingState());
        }
        if (Input.IsActionJustReleased("shoot"))
        {
            weapon.ChangeState(new NoShootingState());
        }
        if (Input.IsActionJustPressed("reload"))
        {
            weapon.ChangeState(new ReloadingState());
        }
        //if (Input.IsActionJustPressed("switch_weapon"))
        //{
        //    weapon.ChangeState(new SwitchingWeaponState());
        //}
    }
}