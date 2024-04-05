using Godot;
using System;
using System.Numerics;

public partial class Slime : CharacterBody2D
{
  private int speed = -60;

  private AnimatedSprite2D anim;
  private PackedScene dissapearsFx;
  private RayCast2D floorDetect;
  private Boolean facingRight = false;
  public override void _Ready()
  {
    anim = GetNode<AnimatedSprite2D>("%Animation");
    anim.Play("walking");
    dissapearsFx = GD.Load<PackedScene>("res://scenes/fx/disappears_fx.tscn");

    floorDetect = GetNode<RayCast2D>("FloorDetect");
  }

  public override void _Process(double delta)
  {
    UpdateMovement(delta);
  }

  public void UpdateMovement(double delta)
  {
    Godot.Vector2 velocity = Velocity;

    velocity.X = speed;

    if (!IsOnFloor())
    {
      velocity.Y += Config.GlobalConfig.GRAVITY * (float)delta;
    }

    if (!floorDetect.IsColliding())
    {
      Flip();
    }

    Velocity = velocity;
    MoveAndSlide();
  }

  public void Flip()
  {
    facingRight = !facingRight;

    Godot.Vector2 scale = Scale;
    scale.X *= -1;
    Scale = scale;

    if (facingRight)
    {
      speed = Math.Abs(speed);
    }
    else
    {
      speed = Math.Abs(speed) * -1;
    }
  }

  public void Die()
  {
    QueueFree();

    Node2D newFx = (Node2D)dissapearsFx.Instantiate();
    newFx.GlobalPosition = GlobalPosition;
    GetParent().AddChild(newFx);
  }
}
