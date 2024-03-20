using Godot;
using System;
using System.Diagnostics;

public partial class Disappears_fx : Node2D
{

  private AnimatedSprite2D anim;
  public override void _Ready()
  {
    anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    anim.Play("default");
  }

  public override void _Process(double delta)
  {
    if (anim.Frame == 3)
    {
      QueueFree();
    }
  }
}
