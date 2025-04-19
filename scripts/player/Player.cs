using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 50.0f;
	public Enums.FaceDirection faceDirection = Enums.FaceDirection.FRONT;
	[Export] public Enums.Tools currentTool = Enums.Tools.NONE;
	[Export] Timer toolTimer;
	[Export] BaseStateMachine stateMachine;
	private bool isHitting = false;
	public void HittingFinish()
	{
		isHitting = false;
		toolTimer.Start();
		PlayerEvent.OnPlayerStateChangeEvent(Enums.State.IDLE);
	}
	public override void _PhysicsProcess(double delta)
	{
		if (isHitting)
			return;
		Enums.State currentState = Enums.State.NONE;
		Vector2 direction = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");
		SetFaceDirection(direction);
		if (Input.IsActionPressed("hit") && !isHitting && toolTimer.IsStopped())
			currentState = UseTool();
		if (currentState == Enums.State.NONE)
			Velocity = Move(direction, out currentState);
		else
			isHitting = true;
		ChangeState(currentState);
		MoveAndSlide();
	}
	private Vector2 Move(Vector2 direction, out Enums.State state)
	{
		Vector2 velocity = Velocity;
		if (direction == Vector2.Up || direction == Vector2.Down)
		{
			velocity.Y = (direction == Vector2.Up ? -1 : 1) * Speed;
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			state = Enums.State.WALK;
		}
		else if (direction == Vector2.Left || direction == Vector2.Right)
		{
			velocity.X = (direction == Vector2.Left ? -1 : 1) * Speed;
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			state = Enums.State.WALK;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			state = Enums.State.IDLE;
		}

		return velocity;
	}
	private Enums.State UseTool()
	{
		return currentTool switch
		{
			Enums.Tools.AXE => Enums.State.CHOPPING,
			Enums.Tools.TILL => Enums.State.TILLING,
			Enums.Tools.WATER => Enums.State.WATERING,
			Enums.Tools.CORN or
			Enums.Tools.TOMATO => Enums.State.NONE,
			_ => Enums.State.NONE,
		};

	}

	private void ChangeState(Enums.State state)
	{
		if (stateMachine.IsSameState(state) || state == Enums.State.NONE) return;
		PlayerEvent.OnPlayerStateChangeEvent(state);
	}
	private void SetFaceDirection(Vector2 direction)
	{
		if (direction.X != 0)
			faceDirection = direction.X > 0 ? Enums.FaceDirection.RIGHT : Enums.FaceDirection.LEFT;
		if (direction.Y != 0)
			faceDirection = direction.Y > 0 ? Enums.FaceDirection.FRONT : Enums.FaceDirection.BACK;
	}
}
