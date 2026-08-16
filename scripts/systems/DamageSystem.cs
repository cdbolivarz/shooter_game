using Godot;
using ShooterGame.Components;

namespace ShooterGame.Systems;

public static class DamageSystem
{
    public static void ApplyDamage(HealthComponent health, DamageComponent damage)
    {
        health.CurrentHealth -= damage.CollitionDamage;
        if (health.CurrentHealth <= 0)
            health.IsAlive = false;

        GD.Print($"Took {damage.CollitionDamage} damage, current health: {health.CurrentHealth}");
    }
}
