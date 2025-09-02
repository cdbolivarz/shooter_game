using System;
using Godot;

public partial class player_controller : CharacterBody2D
{
    [Export]
    public AnimationPlayer _animationPlayerPath;

    public WeaponSystem _weaponSystem;
    public WeaponFactory _weaponFactory;
    public WeaponEntity _currentWeapon;

    private float _runSpeed = 350;
    private float _jumpSpeed = -1000;
    private float _gravity = 2500;



    public override void _Ready()
    {
        _weaponSystem = GetNode<WeaponSystem>("/root/World/WeaponSystem");
        _weaponFactory = GetNode<WeaponFactory>("/root/World/WeaponFactory");
    }

    private void GetInput()
    {
        var velocity = Velocity;
        velocity.X = 0;

        var right = Input.IsActionPressed("ui_right");
        var left = Input.IsActionPressed("ui_left");
        var jump = Input.IsActionPressed("ui_select");



        if (IsOnFloor() && jump)
        {
            velocity.Y = _jumpSpeed;
        }
        if (right)
        {
            velocity.X += _runSpeed;
        }
        if (left)
        {
            velocity.X -= _runSpeed;
        }

        if (velocity.X == 0 && IsOnFloor())
        {
            _animationPlayerPath.Play("idle");
        }
        else
        {
            _animationPlayerPath.Stop();
        }


        Velocity = velocity;

    }

    public override void _Process(double delta)
    {
        
        // Weapon handling
        _weaponSystem.HandleInput(_currentWeapon);

        if (Input.IsActionJustPressed("equip_weapon") && _currentWeapon == null)
        {
            
            var weapon_scene = _weaponFactory.InstantiateWeapon(this, "m16");
            _currentWeapon = weapon_scene.GetNode<WeaponEntity>("WeaponEntity");
            _currentWeapon.Cannon = weapon_scene.GetNode<Marker2D>("Cannon");

        }
        //if (Input.IsActionJustPressed("unequip_weapon") && _currentWeapon != null)
        //{
        //    _currentWeapon.QueueFree();
        //    _currentWeapon = null;
        //}

        if (_currentWeapon != null)
            _weaponSystem.HandleInput(_currentWeapon);

        
    }

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;
        velocity.Y += _gravity * (float)delta;
        Velocity = velocity;
        GetInput();
        MoveAndSlide();
    }

}