public enum PlayerAnimationEnum
{
    Idle,
    Walk,
    Jump,
    Fall,
    Shoot
}

public static class PlayerAnimationExtensions
{
    public static string ToAnimationName(this PlayerAnimationEnum animation)
    {
        return animation switch
        {
            PlayerAnimationEnum.Idle => "idle",
            PlayerAnimationEnum.Walk => "walk",
            PlayerAnimationEnum.Jump => "jump",
            PlayerAnimationEnum.Fall => "fall",
            PlayerAnimationEnum.Shoot => "shoot",
            _ => "idle"
        };
    }
}