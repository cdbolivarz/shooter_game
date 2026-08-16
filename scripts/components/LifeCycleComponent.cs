using ShooterGame.Core;

namespace ShooterGame.Components;

public class LifeCycleComponent : IComponent
{
    public float Duration { get; set; } = -1f;   // seconds, -1 = infinite
    public int MaxCollitions { get; set; } = 1;
    public string OnExpireEffect { get; set; } = "";
    public string OnCollideEffect { get; set; } = "";

    public LifeCycleComponent() { }

    public LifeCycleComponent(LifeCycleComponent copyFrom)
    {
        Duration = copyFrom.Duration;
        MaxCollitions = copyFrom.MaxCollitions;
        OnExpireEffect = copyFrom.OnExpireEffect;
        OnCollideEffect = copyFrom.OnCollideEffect;
    }
}
