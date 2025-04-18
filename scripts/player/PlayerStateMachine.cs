using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerStateMachine : BaseStateMachine
{
    public override void _Ready()
    {
        base._Ready();
    }
    public override void _EnterTree()
    {
        PlayerEvent.playerStateChangeEvent += OnPlayerStateChangeEvent;
    }

    public override void _ExitTree()
    {
        PlayerEvent.playerStateChangeEvent -= OnPlayerStateChangeEvent;
    }

    private void OnPlayerStateChangeEvent(State state)
    {
        ChangeToState(state);
    }

}
