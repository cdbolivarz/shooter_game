using ShooterGame.Core;

namespace ShooterGame.Components;

public enum FireMode
{
    SemiAuto = 0,
    FullAuto = 1,
    Burst = 2
}

public class FireRateComponent : IComponent
{
    public float FireRateDelta { get; set; } = 0.5f;   // seconds between shots
    public FireMode Mode { get; set; } = FireMode.SemiAuto;
    public int BurstCount { get; set; }                // shots per burst when Mode == Burst
}
