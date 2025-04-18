using Godot;
using System;

public partial class PlayerTillingState : PlayerBaseState
{
	public override void _Ready()
	{
		frontName = "tilling_front";
		backName = "tilling_back";
		leftName = "tilling_left";
		rightName = "tilling_right";
		currentAnimationName = frontName;
		state = Enums.State.TILLING;
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
