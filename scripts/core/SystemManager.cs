using System.Collections.Generic;

namespace ShooterGame.Core;

/// <summary>
/// Owns an ordered collection of <see cref="ISystem"/>s and ticks them each frame.
/// Reusable both globally (on the World root) and per-entity.
/// </summary>
public class SystemManager
{
    private readonly List<ISystem> _systems = new();

    public void Add(ISystem system) => _systems.Add(system);

    public bool Remove(ISystem system) => _systems.Remove(system);

    public void Clear() => _systems.Clear();

    public void Update(double delta)
    {
        for (int i = 0; i < _systems.Count; i++)
            _systems[i].Update(delta);
    }
}
