using Godot;
using System;

public partial class BaseState : Node
{
	protected BaseStateMachine stateMachine;
	protected Node stateOwner;
	protected State state;
	public State GetState() { return state; }
	/// <summary>
	/// 初始化
	/// </summary>
	/// <param name="_stateMachine">当前所处状态机</param>
	/// <param name="_stateOwner">当前所处状态机的服父物体，比如玩家</param>
	public virtual void Initialize(BaseStateMachine _stateMachine, Node _stateOwner)
	{
		stateMachine = _stateMachine;
		stateOwner = _stateOwner;
	}
	/// <summary>
	/// 进入状态时调用
	/// </summary>
	public virtual void OnStateEnter()
	{

	}
	/// <summary>
	/// 每一帧调用一次
	/// </summary>
	/// <param name="delta"></param>
	public virtual void OnStateFrameUpdate(float delta)
	{

	}
	/// <summary>
	/// 固定时间调用一次
	/// </summary>
	/// <param name="delta"></param>
	public virtual void OnStatePhysicsUpdate(float delta)
	{

	}
	/// <summary>
	/// 退出调用一次
	/// </summary>
	public virtual void OnStateExit()
	{

	}
}
