using Godot;


public partial class EnemyController : CharacterBody2D, IDamagable, IWeapons
{
    [Export] public int MaxHealth { get; set; } = 100;
    [Export] public string[] weaponInventory = new string[] { "m16" };

    public EnemyEntity Enemy { get; set; }
    public DamageSystem damageSystem { get; set; }
    public WeaponSystem weaponSystem { get; set; }
    public EnemySystems Systems { get; set; }

    public override void _Ready()
    {
        Enemy = new EnemyEntity();
        Enemy.Health = new HealthComponent(MaxHealth);
        damageSystem = new DamageSystem(Enemy.Health);
        weaponSystem = new WeaponSystem(weaponInventory);
        Systems = new EnemySystems(damageSystem, weaponSystem);
        CallDeferred("initializeWeapon");

    }

    public override void _Process(double delta)
    {
        Systems.weaponSystem.TryShoot();

        if (!Enemy.Health.IsAlive)
        {
            QueueFree();
        }
    }

    private void initializeWeapon()
    {
        Systems.weaponSystem.EquipWeapon(this);

        Systems.weaponSystem.currentWeapon.WeaponSprite.FlipH = true;

        // Esto deberia ser un metodo de rotacion del arma
        Vector2 weapon_position = Systems.weaponSystem.currentWeapon.Cannon.Position;
        float x_cannon_positon = -1 * weapon_position.X;
        Systems.weaponSystem.currentWeapon.Cannon.Position = new Vector2(x_cannon_positon, weapon_position.Y);

        Vector2 projectile_linear_speed = Systems.weaponSystem.currentWeapon.Projectile.LinearSpeed;
        float x_linear_speed = -1 * projectile_linear_speed.X;
        Systems.weaponSystem.currentWeapon.Projectile.LinearSpeed = new Vector2(x_linear_speed, projectile_linear_speed.Y);

    }

}