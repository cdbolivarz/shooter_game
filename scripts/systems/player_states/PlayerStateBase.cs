using Godot;

public abstract class PlayerStateBase : IPlayerState
{
    protected CharacterBody2D player;
    protected AnimationPlayer animationPlayer;
    protected CharacterAttributes attributes;
    
    protected PlayerStateBase(CharacterBody2D player, AnimationPlayer animationPlayer, CharacterAttributes attributes)
    {
        this.player = player;
        this.animationPlayer = animationPlayer;
        this.attributes = attributes;
    }
    
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update(float delta) { }
    
    public abstract void HandleMove(Vector2 inputDirection);
    public abstract void HandleJump();
    public abstract void HandleShoot();
    
    public virtual PlayerStateType GetNextStateType() => PlayerStateType.None;
}
