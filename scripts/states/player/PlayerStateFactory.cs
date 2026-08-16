using System;
using System.Collections.Generic;
using ShooterGame.Entities;

namespace ShooterGame.States.Player;

/// <summary>
/// Creates player states on demand. To add a state: write one state class
/// and register it here.
/// </summary>
public class PlayerStateFactory
{
    private readonly Dictionary<PlayerStateType, Func<IPlayerState>> _factories;

    public PlayerStateFactory(PlayerController player)
    {
        _factories = new Dictionary<PlayerStateType, Func<IPlayerState>>
        {
            [PlayerStateType.Ground] = () => new GroundState(player),
            [PlayerStateType.Airborne] = () => new AirborneState(player),
        };
    }

    public IPlayerState Create(PlayerStateType type) =>
        _factories.TryGetValue(type, out Func<IPlayerState> factory) ? factory() : null;
}
