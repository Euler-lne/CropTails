using Godot;
using System;

public partial class PlayerIdleState : PlayerBaseState
{
	public override void _Ready()
	{
		frontName = "idle_front";
		backName = "idle_back";
		leftName = "idle_left";
		rightName = "idle_right";
		currentAnimationName = frontName;
		state = Enums.State.IDLE;
	}

}
