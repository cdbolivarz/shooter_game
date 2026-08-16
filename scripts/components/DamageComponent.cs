using ShooterGame.Core;

namespace ShooterGame.Components;

public class DamageComponent : IComponent
{
    public float DamagePerSecond { get; set; }
    public bool IsAreaEffect { get; set; }
    public float AreaRadius { get; set; }
    public float CollitionDamage { get; set; }

    public DamageComponent() { }

    public DamageComponent(DamageComponent copyFrom)
    {
        DamagePerSecond = copyFrom.DamagePerSecond;
        IsAreaEffect = copyFrom.IsAreaEffect;
        AreaRadius = copyFrom.AreaRadius;
        CollitionDamage = copyFrom.CollitionDamage;
    }
}
