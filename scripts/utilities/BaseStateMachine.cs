using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class BaseStateMachine : Node
{
	[Export]
	private BaseState startState;
	private BaseState currentState;
	private readonly Dictionary<State, BaseState> stateDict = [];
	public override void _Ready()
	{
		foreach (BaseState state in GetChildren().Cast<BaseState>())
		{
			state.Initialize(this, GetParent<Node>());
			stateDict.Add(state.GetState(), state);
		}
		LaunchStateMachine();
	}
	private void LaunchStateMachine()
	{
		currentState = startState;
		currentState.OnStateEnter();
	}
	public override void _Process(double delta)
	{
		currentState?.OnStateFrameUpdate((float)delta);
	}
	public override void _PhysicsProcess(double delta)
	{
		currentState?.OnStatePhysicsUpdate((float)delta);
	}

	protected void ChangeToState(State state)
	{
		if (!stateDict.TryGetValue(state, out BaseState newState))
		{
			GD.Print("不存在的值" + state.ToString());
			return;
		}
		if (newState == currentState) return;
		currentState.OnStateExit();
		currentState = newState;
		currentState.OnStateEnter();
	}

}
