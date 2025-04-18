using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 200.0f;
	public FaceDirection faceDirection = FaceDirection.FRONT;
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");
		SetFaceDirection(direction);
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;

			PlayerEvent.OnPlayerStateChangeEvent(State.WALK);
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			PlayerEvent.OnPlayerStateChangeEvent(State.IDLE);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void SetFaceDirection(Vector2 direction)
	{
		if (direction.X != 0)
			faceDirection = direction.X > 0 ? FaceDirection.RIGHT : FaceDirection.LEFT;
		if (direction.Y != 0)
			faceDirection = direction.Y > 0 ? FaceDirection.FRONT : FaceDirection.BACK;
	}
}
