using Godot;
using ShooterGame.Components;
using ShooterGame.Core;
using ShooterGame.Entities;
using ShooterGame.Input;

namespace ShooterGame.States.Player;

public abstract class PlayerStateBase : IPlayerState
{
    protected readonly PlayerController Player;
    protected readonly AnimationPlayer AnimationPlayer;
    protected readonly MovementComponent Movement;

    protected PlayerStateBase(PlayerController player)
    {
        Player = player;
        AnimationPlayer = player.AnimationPlayer;
        Movement = player.Components.Get<MovementComponent>();
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update(float delta) { }
    public virtual void HandleInputAction(InputAction action) { }
    public virtual void HandleMovement(Vector2 inputDirection) { }
    public virtual PlayerStateType? NextStateType => null;

    /// <summary>Forwards weapon-related input to the weapon state machine.</summary>
    protected void ForwardWeaponInput(InputAction action)
    {
        switch (action)
        {
            case InputAction.Shoot:
            case InputAction.Reload:
            case InputAction.SwitchWeapon:
            case InputAction.ShootReleased:
                Player.WeaponSystem.HandleAction(action);
                break;
        }
    }

    protected void EquipWeaponIfEmpty()
    {
        if (Player.WeaponSystem.CurrentWeapon == null)
            Player.WeaponSystem.EquipWeapon();
    }
}
