
public class EnemySystems : IDamagable, IWeapons
{
    public DamageSystem damageSystem { get; set; }
    public WeaponSystem weaponSystem { get; set; }

    public EnemySystems(DamageSystem damageSystem = null, WeaponSystem weaponSystem = null)
    {
        this.damageSystem = damageSystem;
        this.weaponSystem = weaponSystem;
    }
}
