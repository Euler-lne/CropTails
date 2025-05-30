using Godot;
using System;
using System.Diagnostics;

public partial class PlayerChoppingState : PlayerBaseState
{
	private bool isFirst = false;
	public override void _Ready()
	{
		frontName = "chopping_front";
		backName = "chopping_back";
		leftName = "chopping_left";
		rightName = "chopping_right";
		currentAnimationName = frontName;
		state = Enums.State.CHOPPING;
	}
	public override void OnStateFrameUpdate(float delta)
	{
		base.OnStateFrameUpdate(delta);
		if (animatedSprite2D != null && animatedSprite2D.Frame == 1 && isFirst)
		{
			((Player)stateOwner).SetHitCollitionActive();
			isFirst = false;
		}
	}
	public override void OnStateEnter()
	{
		base.OnStateEnter();
		animatedSprite2D.AnimationFinished += OnAnimationFinished;
		isFirst = true;
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
