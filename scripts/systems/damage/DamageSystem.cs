using Godot;
public class DamageSystem
{
    HealthComponent healthComponent { get; set; }
    public DamageSystem(HealthComponent healthComponent)
    {
        this.healthComponent = healthComponent;
    }

    public void ApplyDamage(DamageComponent damage)
    {

        healthComponent.CurrentHealth -= (int)damage.CollitionDamage;
        if (healthComponent.CurrentHealth <= 0)
        {
            healthComponent.IsAlive = false;
        }
        // We could register an signal here and notify other systems of the damage taken (text, sound, etc)
        // More complex damage calculations could be done here (resistances, critical hits, time based damage, etc)
        GD.Print($"Took {damage.CollitionDamage} damage, current health: {healthComponent.CurrentHealth}");
    }
}
