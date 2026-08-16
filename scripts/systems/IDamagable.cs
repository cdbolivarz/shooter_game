using ShooterGame.Components;

namespace ShooterGame.Systems;

/// <summary>Capability marker: an entity that can take damage exposes its <see cref="HealthComponent"/>.</summary>
public interface IDamagable
{
    HealthComponent Health { get; }
}
