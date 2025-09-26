using Godot;
using System.Collections.Generic;

// Singleton pattern for weapon database
public partial class WeaponDatabase: Node
{
    private Dictionary<string, WeaponData> _weapons = new();
    private static WeaponDatabase _instance;

    private WeaponDatabase()
    {
        // Preload or load dynamically
        RegisterWeapon(ResourceLoader.Load<WeaponData>("res://databases/weapons/M16.tres"));
        RegisterWeapon(ResourceLoader.Load<WeaponData>("res://databases/weapons/FAMAS.tres"));
        RegisterWeapon(ResourceLoader.Load<WeaponData>("res://databases/weapons/CANNON.tres"));
        // RegisterWeapon(ResourceLoader.Load<WeaponData>("res://Data/Weapons/Shotgun.tres"));
    }

    public override void _Ready()
    {
        _instance = new WeaponDatabase();
        GD.Print("[WeaponDatabase] Initialized with weapons: " + string.Join(", ", _weapons.Keys));
    }

    public WeaponDatabase GetInstance()
    {
        return _instance;
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