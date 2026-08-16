namespace ShooterGame.Core;

/// <summary>
/// A behavior unit that runs each frame. Systems read/write component data
/// and contain no Godot scene references unless passed one explicitly.
/// </summary>
public interface ISystem
{
    void Update(double delta);
}
