using Godot;


public partial class EnemyController : CharacterBody2D
{
    [Export] public int MaxHealth { get; set; } = 100;
    public EnemyEntity Enemy { get; set; }

    public override void _Ready()
    {
        Enemy = new EnemyEntity();
        Enemy.Health = new HealthComponent(MaxHealth);
    }

    public override void _Process(double delta)
    {
        if (!Enemy.Health.IsAlive)
        {
            QueueFree();
        }
    }

    public void TakeDamage(int damage)
    {
        Enemy.Health.CurrentHealth -= damage;
        if (Enemy.Health.CurrentHealth <= 0)
        {
            Enemy.Health.IsAlive = false;
        }
        GD.Print($"Enemy took {damage} damage, current health: {Enemy.Health.CurrentHealth}");
    }

}