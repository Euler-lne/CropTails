using Godot;
using System;

public partial class PlayerWateringState : PlayerBaseState
{
	public override void _Ready()
	{
		frontName = "watering_front";
		backName = "watering_back";
		leftName = "watering_left";
		rightName = "watering_right";
		currentAnimationName = frontName;
		state = Enums.State.WATERING;
	}

	public override void OnStateEnter()
	{
		base.OnStateEnter();
		animatedSprite2D.AnimationFinished += OnAnimationFinished;
	}
	public override void OnStateExit()
	{
		base.OnStateExit();
		animatedSprite2D.AnimationFinished -= OnAnimationFinished;
	}
	private void OnAnimationFinished()
	{
		((Player)stateOwner).HittingFinish();
	}

}
