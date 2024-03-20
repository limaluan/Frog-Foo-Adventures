using Godot;
using System;

public partial class Cherry : Area2D
{
  private PackedScene dissapearFx;
  private Timer _timer;
  public override void _Ready()
  {
    GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("default");
		dissapearFx = GD.Load<PackedScene>("res://scenes/fx/disappears_fx.tscn");
  }

  public void OnTakeCherry(Node2D body)
  {
    if (body.HasMethod("TakeCherry"))
    {
      body.Call("TakeCherry");
			QueueFree();
			
			Node2D newFx = (Node2D)dissapearFx.Instantiate();
			newFx.GlobalPosition = GlobalPosition;
      GetParent().AddChild(newFx);
    }
  }
}
