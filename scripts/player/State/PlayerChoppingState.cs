using Godot;
using System;

public partial class PlayerChoppingState : PlayerBaseState
{
	public override void _Ready()
	{
		frontName = "chopping_front";
		backName = "chopping_back";
		leftName = "chopping_left";
		rightName = "chopping_right";
		currentAnimationName = frontName;
		state = Enums.State.CHOPPING;
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
