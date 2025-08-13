using Godot;
using System;

public partial class animation_player : AnimationPlayer
{
    public override void _Ready()
    {
        Play("idle");
    }
}
