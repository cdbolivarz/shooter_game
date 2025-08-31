using Godot;

public class PlayerStateMachine
{
    private IPlayerState currentState;
    private PlayerStateFactory stateFactory;
    
    public IPlayerState CurrentState => currentState;
    
    public PlayerStateMachine(PlayerStateFactory stateFactory)
    {
        this.stateFactory = stateFactory;
    }
    
    public void Initialize(PlayerStateType initialStateType)
    {
        currentState = stateFactory.CreateState(initialStateType);
        currentState?.Enter();
    }
    
    public void Update(float delta)
    {
        currentState?.Update(delta);
        
        PlayerStateType nextStateType = currentState?.GetNextStateType() ?? PlayerStateType.None;
        if (nextStateType != PlayerStateType.None)
        {
            TransitionTo(nextStateType);
        }
    }
    
    private void TransitionTo(PlayerStateType newStateType)
    {
        IPlayerState newState = stateFactory.CreateState(newStateType);
        if (newState != null)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }
    }
}
