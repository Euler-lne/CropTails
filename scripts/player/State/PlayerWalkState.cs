using Godot;
using System;

public partial class PlayerWalkState : PlayerBaseState
{
	public override void _Ready()
	{
		frontName = "walk_front";
		backName = "walk_back";
		leftName = "walk_left";
		rightName = "walk_right";
		currentAnimationName = frontName;
		state = Enums.State.WALK;
	}
}
