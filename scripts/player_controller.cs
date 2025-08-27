using System;
using Godot;

public partial class player_controller : CharacterBody2D
{
    [Export]
    public AnimationPlayer _animationPlayerPath;

    public WeaponSystem WeaponS;
    public WeaponComponent Gun;

    private float _runSpeed = 350;
    private float _jumpSpeed = -1000;
    private float _gravity = 2500;



    public override void _Ready()
    {
        WeaponS = GetNode<WeaponSystem>("/root/World/WeaponSystem");
        Gun = GetNode<WeaponComponent>("Gun");
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
        if (Input.IsActionPressed("shoot"))
        {
            WeaponS.TryShoot(this, Gun);
        }
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