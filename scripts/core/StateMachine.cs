using System;

namespace ShooterGame.Core;

/// <summary>
/// State contract. After <see cref="Update"/> runs, the machine polls
/// <see cref="NextStateType"/>; a non-null value triggers a transition.
/// </summary>
public interface IState<TStateType> where TStateType : struct, Enum
{
    void Enter();
    void Exit();
    void Update(float delta);
    TStateType? NextStateType { get; }
}

/// <summary>
/// Generic finite state machine. Removes the duplicated transition plumbing
/// (Initialize / Update / TransitionTo) that was copy-pasted between the
/// player and weapon machines. States are created on demand via a factory
/// delegate so new states require no machine changes.
/// </summary>
public class StateMachine<TStateType, TState>
    where TStateType : struct, Enum
    where TState : class, IState<TStateType>
{
    private readonly Func<TStateType, TState> _createState;
    private TState _current;

    public TState Current => _current;

    public StateMachine(Func<TStateType, TState> createState)
    {
        _createState = createState;
    }

    public void Initialize(TStateType initialState) => TransitionTo(initialState);

    public void Update(float delta)
    {
        if (_current == null)
            return;

        _current.Update(delta);

        if (_current.NextStateType is { } next)
            TransitionTo(next);
    }

    public void TransitionTo(TStateType stateType)
    {
        TState next = _createState(stateType);
        if (next == null)
            return;

        _current?.Exit();
        _current = next;
        _current.Enter();
    }
}
