using Godot;

public partial class FireRateComponent : Node
{
    [Export] public float FireRateDelta { get; set; } = 0.5f; // time unity: seconds 
    [Export] public int Mode { get; set; } = 0; // 1 = semi-auto, 2 = full-auto, 3 = burst. We can add more modes later for mods 


    public int BurstCount { get; set; } = 0; // number of shots in a burst

}


