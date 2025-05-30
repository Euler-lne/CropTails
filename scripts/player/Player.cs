using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 50.0f;
	public Enums.FaceDirection faceDirection = Enums.FaceDirection.FRONT;
	[Export] public Enums.Tools currentTool = Enums.Tools.NONE;
	[Export] Timer toolTimer;
	[Export] BaseStateMachine stateMachine;
	[Export] CollisionShape2D hitCollision;
	[Export] Node2D frontHitPos;
	[Export] Node2D leftHitPos;
	[Export] Node2D rightHitPos;
	[Export] Node2D backHitPos;

	private bool isHitting = false;

	private bool _isPressHit = false;
	private Vector2 _direction = Vector2.Zero;

	public override void _Ready()
	{
		hitCollision.Disabled = true;
		hitCollision.Position = frontHitPos.Position;
	}
	public override void _Process(double delta)
	{
		_direction = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");
		_isPressHit = Input.IsActionPressed("hit");
		UpdateState();
	}
	public override void _PhysicsProcess(double delta)
	{
		if (isHitting)
			return;
		SetFaceDirection(_direction);
		Velocity = Move(_direction);
		MoveAndSlide();
	}
	private void UpdateState()
	{
		if (isHitting)
			return;
		Enums.State currentState = Enums.State.NONE;
		if (_isPressHit && !isHitting && toolTimer.IsStopped())
			currentState = UseTool();
		if (currentState == Enums.State.NONE)
			currentState = Velocity.Length() <= 0.000001 ? Enums.State.IDLE : Enums.State.WALK;
		else
			isHitting = true;
		ChangeState(currentState);
	}
	private Vector2 Move(Vector2 direction)
	{
		Vector2 velocity = Velocity;
		if (direction == Vector2.Up || direction == Vector2.Down)
		{
			velocity.Y = (direction == Vector2.Up ? -1 : 1) * Speed;
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		else if (direction == Vector2.Left || direction == Vector2.Right)
		{
			velocity.X = (direction == Vector2.Left ? -1 : 1) * Speed;
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
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
		SetHitCollisionPos();
	}
	private void SetHitCollisionPos()
	{
		switch (faceDirection)
		{
			case Enums.FaceDirection.FRONT:
				hitCollision.Position = frontHitPos.Position;
				break;
			case Enums.FaceDirection.BACK:
				hitCollision.Position = backHitPos.Position;
				break;
			case Enums.FaceDirection.LEFT:
				hitCollision.Position = leftHitPos.Position;
				break;
			case Enums.FaceDirection.RIGHT:
				hitCollision.Position = rightHitPos.Position;
				break;
		}
	}

	public void HittingFinish()
	{
		hitCollision.Disabled = true;
		isHitting = false;
		toolTimer.Start();
		PlayerEvent.OnPlayerStateChangeEvent(Enums.State.IDLE);
	}
	public void SetHitCollitionActive()
	{
		hitCollision.Disabled = false;
	}
}
