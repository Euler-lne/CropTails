using Godot;
using System;

public partial class PlayerBaseState : BaseState
{
    private AnimatedSprite2D animatedSprite2D = null;
    private string currentAnimationName;
    protected string frontName, backName, leftName, rightName;

    public override void OnStateEnter()
    {
        animatedSprite2D ??= stateOwner.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite2D.Play(currentAnimationName);
    }
    /// <summary>
    /// 每一帧调用一次
    /// </summary>
    /// <param name="delta"></param>
    public override void OnStateFrameUpdate(float delta)
    {
        string animationName = ((Player)stateOwner).faceDirection switch
        {
            FaceDirection.FRONT => frontName,
            FaceDirection.BACK => backName,
            FaceDirection.LEFT => leftName,
            FaceDirection.RIGHT => rightName,
            _ => throw new NotImplementedException(),
        };
        if (animationName != currentAnimationName)
        {
            currentAnimationName = animationName;
            animatedSprite2D.Play(currentAnimationName);
        }
    }
    /// <summary>
    /// 固定时间调用一次
    /// </summary>
    /// <param name="delta"></param>
    public override void OnStatePhysicsUpdate(float delta)
    {

    }
    /// <summary>
    /// 退出调用一次
    /// </summary>
    public override void OnStateExit()
    {

    }
}
