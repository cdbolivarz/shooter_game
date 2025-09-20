using Godot;

public class EnemyEntity
{
    public string Name { get; set; } = "Enemy";
    public HealthComponent Health { get; set; } = new HealthComponent();
    // These should be components
    public float Speed { get; set; } = 100.0f;
    public int Damage { get; set; } = 10;
    public float DetectionRange { get; set; } = 200.0f;
    public float AttackRange { get; set; } = 50.0f;
    public float AttackCooldown { get; set; } = 1.5f;
    public bool IsAggressive { get; set; } = true;

    public EnemyEntity() { }

    public EnemyEntity(string name, HealthComponent health, float speed, int damage, float detectionRange, float attackRange, float attackCooldown, bool isAggressive)
    {
        Name = name;
        Health = health;
        Speed = speed;
        Damage = damage;
        DetectionRange = detectionRange;
        AttackRange = attackRange;
        AttackCooldown = attackCooldown;
        IsAggressive = isAggressive;
    }

}