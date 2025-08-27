using System;
using Godot;

public partial class player_controller : CharacterBody2D
{
    [Export]
    public AnimationPlayer _animationPlayerPath;

    public WeaponSystem WeaponS;
    public WeaponComponent Gun;
    public Marker2D Cannon;

    private float _runSpeed = 350;
    private float _jumpSpeed = -1000;
    private float _gravity = 2500;



    public override void _Ready()
    {
        WeaponS = GetNode<WeaponSystem>("/root/World/WeaponSystem");
        Gun = GetNode<WeaponComponent>("Gun");
        Cannon = GetNode<Marker2D>("Gun/Cannon");
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
        // state machine for weapons?, reloading, switching, shooting modes, overheating, etc
        if (Input.IsActionPressed("shoot"))
        {
            WeaponS.TryShoot(Cannon, Gun);
        }
        if (Input.IsActionJustPressed("reload"))
        {
            // Async call to avoid blocking the main thread while reloading
            _ = WeaponS.Reload(Gun);
        }
        //if (Input.IsActionJustPressed("switch_weapon"))
        //{
            // WeaponS.SwitchWeapon(); // only 2 weapons?, or cycle through a list?, or open a menu?, slowmotion while selecting?
        //}
        //if (Input.IsActionJustPressed("switch_firemode"))
        //{
            // WeaponS.SwitchFireMode(Gun); // only 2 modes?, or cycle through a list?
        //}
        
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