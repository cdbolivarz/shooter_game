using System;
using System.Collections.Generic;

namespace ShooterGame.Core;

/// <summary>
/// Typed container for an entity's data components. One instance per type.
/// Systems query components through this bag, which is what makes adding
/// a new component a single-file change with no ripple edits.
/// </summary>
public class ComponentBag
{
    private readonly Dictionary<Type, IComponent> _components = new();

    public T Add<T>(T component) where T : class, IComponent
    {
        _components[typeof(T)] = component;
        return component;
    }

    public T Get<T>() where T : class, IComponent
    {
        if (_components.TryGetValue(typeof(T), out var component))
            return (T)component;
        throw new InvalidOperationException($"Component {typeof(T).Name} not found on entity.");
    }

    public bool TryGet<T>(out T component) where T : class, IComponent
    {
        if (_components.TryGetValue(typeof(T), out var found))
        {
            component = (T)found;
            return true;
        }

        component = null;
        return false;
    }

    public bool Has<T>() where T : class, IComponent => _components.ContainsKey(typeof(T));

    public bool Remove<T>() where T : class, IComponent => _components.Remove(typeof(T));
}
