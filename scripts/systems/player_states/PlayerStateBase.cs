using Godot;

public abstract class PlayerStateBase : IPlayerState
{
    protected CharacterBody2D player;
    protected AnimationPlayer animationPlayer;
    protected CharacterAttributes attributes;
    protected PlayerSystems playerSystems;

    protected PlayerStateBase(CharacterBody2D player, AnimationPlayer animationPlayer, CharacterAttributes attributes, PlayerSystems playerSystems)
    {
        this.player = player;
        this.animationPlayer = animationPlayer;
        this.attributes = attributes;
        this.playerSystems = playerSystems;
    }
    
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update(float delta) { }
    
    public abstract void HandleInputAction(InputAction action);
    public abstract void HandleMovement(Vector2 inputDirection);
    
    public virtual PlayerStateType GetNextStateType() => PlayerStateType.None;
}
