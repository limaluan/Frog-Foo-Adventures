using Godot;
using System;

namespace Entities
{
  public partial class Player : CharacterBody2D
  {
    [Export]
    public int Speed { get; set; } = 400;
    [Export]
    public float JumpForce { get; set; } = 400;
    private AnimatedSprite2D anim;

    public override void _Ready()
    {
      anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
      UpdateMovement(delta);
      UpdateAnimation();
    }

    public void UpdateMovement(double delta)
    {
      Vector2 velocity = Velocity;

      velocity.X = (Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left")) * Speed;

      // Aplica Gravidade
      if (!IsOnFloor())
      {
        velocity.Y += Config.GlobalConfig.GRAVITY * (float)delta;
      }

      // Mecânica de Pulo
      if (IsOnFloor() && Input.IsActionJustPressed("jump"))
      {
        velocity.Y -= JumpForce;
      }

      Velocity = velocity;
      MoveAndSlide();
    }

    public void UpdateAnimation()
    {
      // Lógica de Animação ao correr
      if (Velocity.X != 0 && IsOnFloor())
      {
        anim.Play("run");
      }
      else if (Velocity.X == 0 && IsOnFloor())
      {
        anim.Play("idle");
      }

      // Animação de pulo
      if (Velocity.Y < 0)
      {
        anim.Play("jump");
      }
      else if (Velocity.Y > 0 && !IsOnFloor())
      {
        anim.Play("fall");
      }

      // Vira direção do Sprite
      if (Velocity.X < 0)
      {
        anim.FlipH = true;
      }
      else if (Velocity.X > 0)
      {
        anim.FlipH = false;
      }
    }
  }
}
