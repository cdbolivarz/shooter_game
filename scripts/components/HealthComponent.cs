using ShooterGame.Core;

namespace ShooterGame.Components;

public class HealthComponent : IComponent
{
    public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; } = 100f;
    public bool IsAlive { get; set; } = true;

    public HealthComponent() { }

    public HealthComponent(float maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        IsAlive = true;
    }
}
