using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class BaseStateMachine : Node
{
	[Export]
	private Enums.State startState;
	private Enums.State currentState;
	private readonly Dictionary<Enums.State, BaseState> stateDict = [];
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
		stateDict[currentState].OnStateEnter();
	}
	public override void _Process(double delta)
	{
		stateDict[currentState]?.OnStateFrameUpdate((float)delta);
	}
	public override void _PhysicsProcess(double delta)
	{
		stateDict[currentState]?.OnStatePhysicsUpdate((float)delta);
	}

	protected void ChangeToState(Enums.State state)
	{
		if (!stateDict.ContainsKey(state))
		{
			GD.Print("不存在的值" + state.ToString());
			return;
		}
		if (state == currentState) return;
		stateDict[currentState].OnStateExit();
		currentState = state;
		stateDict[currentState].OnStateEnter();
	}
	public bool IsSameState(Enums.State state)
	{
		return state == currentState;
	}
}
