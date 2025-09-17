using Godot;

public class WeaponStateMachine
{
    private IWeaponState currentState;
    private WeaponStateFactory stateFactory;
    
    public IWeaponState CurrentState => currentState;
    
    public WeaponStateMachine(WeaponStateFactory stateFactory)
    {
        this.stateFactory = stateFactory;
    }
    
    public void Initialize(WeaponStateType initialStateType)
    {
        currentState = stateFactory.CreateState(initialStateType);
        currentState?.Enter();
    }
    
    public void Update(float delta)
    {
        currentState?.Update(delta);
        WeaponStateType nextStateType = currentState?.GetNextStateType() ?? WeaponStateType.None;
        if (nextStateType != WeaponStateType.None)
        {
            TransitionTo(nextStateType);
        }
    }
    
    public void HandleAction(InputAction action)
    {
        currentState?.HandleAction(action);
    }
    
    private void TransitionTo(WeaponStateType newStateType)
    {
        IWeaponState newState = stateFactory.CreateState(newStateType);
        if (newState != null)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }
    }
}
