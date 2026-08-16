namespace ShooterGame.Core;

/// <summary>
/// Tags a Godot node as a game entity that owns a <see cref="ComponentBag"/>.
/// </summary>
public interface IEntity
{
    ComponentBag Components { get; }
}
