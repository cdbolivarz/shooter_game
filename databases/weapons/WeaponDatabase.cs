using Godot;
using System.Collections.Generic;

public partial class WeaponDatabase : Node
{
    private Dictionary<string, WeaponData> _weapons = new();

    public override void _Ready()
    {
        // Preload or load dynamically
        RegisterWeapon(ResourceLoader.Load<WeaponData>("res://databases/weapons/M16.tres"));
        // RegisterWeapon(ResourceLoader.Load<WeaponData>("res://Data/Weapons/Shotgun.tres"));
    }

    private void RegisterWeapon(WeaponData data)
    {
        if (!_weapons.ContainsKey(data.Id))
            _weapons[data.Id] = data;
    }

    public WeaponData GetWeaponData(string id)
    {
        return _weapons.GetValueOrDefault(id);
    }
}