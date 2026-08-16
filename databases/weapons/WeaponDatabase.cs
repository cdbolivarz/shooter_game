using Godot;
using System.Collections.Generic;

namespace ShooterGame.Data;

/// <summary>
/// Godot autoload singleton (registered in project.godot). Holds all weapon
/// definitions; access via <see cref="Instance"/>.
/// </summary>
public partial class WeaponDatabase : Node
{
    public static WeaponDatabase Instance { get; private set; }

    private readonly Dictionary<string, WeaponData> _weapons = new();

    public override void _EnterTree()
    {
        Instance = this;

        RegisterWeapon(ResourceLoader.Load<WeaponData>("res://databases/weapons/M16.tres"));
        RegisterWeapon(ResourceLoader.Load<WeaponData>("res://databases/weapons/FAMAS.tres"));
        RegisterWeapon(ResourceLoader.Load<WeaponData>("res://databases/weapons/CANNON.tres"));
    }

    public WeaponData GetWeaponData(string id) => _weapons.GetValueOrDefault(id);

    private void RegisterWeapon(WeaponData data)
    {
        if (data != null && !_weapons.ContainsKey(data.Id))
            _weapons[data.Id] = data;
    }
}
