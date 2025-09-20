public class HealthComponent
{
    public float MaxHealth { get; set; } = 100;
    public float CurrentHealth { get; set; } = 100;
    public bool IsAlive { get; set; } = true;

    public HealthComponent() { }

    public HealthComponent(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        IsAlive = true;
    }

}