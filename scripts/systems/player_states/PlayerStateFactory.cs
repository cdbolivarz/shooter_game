using Godot;

public class PlayerStateFactory
{
    private CharacterBody2D player;
    private AnimationPlayer animationPlayer;
    private CharacterAttributes attributes;
    
    public PlayerStateFactory(CharacterBody2D player, AnimationPlayer animationPlayer, CharacterAttributes attributes)
    {
        this.player = player;
        this.animationPlayer = animationPlayer;
        this.attributes = attributes;
    }
    
    public IPlayerState CreateState(PlayerStateType stateType)
    {
        return stateType switch
        {
            PlayerStateType.Ground => new GroundState(player, animationPlayer, attributes),
            PlayerStateType.Airborne => new AirborneState(player, animationPlayer, attributes),
            _ => null
        };
    }
}
