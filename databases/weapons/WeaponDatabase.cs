using Godot;
using System.Collections.Generic;

// Singleton pattern for weapon database
public sealed class WeaponDatabase
{
    private Dictionary<string, WeaponData> _weapons = new();
    private static WeaponDatabase _instance;

    private WeaponDatabase()
    {
        // Preload or load dynamically
        RegisterWeapon(ResourceLoader.Load<WeaponData>("res://databases/weapons/M16.tres"));
        // RegisterWeapon(ResourceLoader.Load<WeaponData>("res://Data/Weapons/Shotgun.tres"));
    }

    public static WeaponDatabase GetInstance()
    {
        if (_instance == null)
        {
            _instance = new WeaponDatabase();
        }
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