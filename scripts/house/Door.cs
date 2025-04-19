using Godot;
using System;

public partial class Door : StaticBody2D
{
	private uint originalCollisionLayer;
	private AnimatedSprite2D animatedSprite2D;
	private Area2D doorArea;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		doorArea = GetNode<Area2D>("Area2D");
		doorArea.BodyEntered += OnBodyEntered;
		doorArea.BodyExited += OnBodyExited;
		originalCollisionLayer = CollisionLayer;
	}

	private void OnBodyExited(Node2D body)
	{
		if (body is Player player)
		{
			animatedSprite2D.Play("close");
			CollisionLayer = originalCollisionLayer;
		}
	}


	private void OnBodyEntered(Node2D body)
	{

		if (body is Player player)
		{
			animatedSprite2D.Play("open");
			CollisionLayer = player.CollisionLayer;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _ExitTree()
	{
		doorArea.AreaEntered -= OnBodyEntered;
		doorArea.AreaExited -= OnBodyEntered;
	}

}
