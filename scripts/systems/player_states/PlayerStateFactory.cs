using Godot;

public class PlayerStateFactory
{
    private CharacterBody2D player;
    private AnimationPlayer animationPlayer;
    private CharacterAttributes attributes;
    private PlayerSystems playerSystems;

    public PlayerStateFactory(CharacterBody2D player, AnimationPlayer animationPlayer, CharacterAttributes attributes, PlayerSystems playerSystems)
    {
        this.player = player;
        this.animationPlayer = animationPlayer;
        this.attributes = attributes;
        this.playerSystems = playerSystems;
    }
    
    public IPlayerState CreateState(PlayerStateType stateType)
    {
        return stateType switch
        {
            PlayerStateType.Ground => new GroundState(player, animationPlayer, attributes, playerSystems),
            PlayerStateType.Airborne => new AirborneState(player, animationPlayer, attributes, playerSystems),
            _ => null
        };
    }
}
