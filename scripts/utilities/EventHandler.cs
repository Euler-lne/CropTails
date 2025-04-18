using System;

public class PlayerEvent
{
    public static Action<State> playerStateChangeEvent;
    public static void OnPlayerStateChangeEvent(State state)
    {
        playerStateChangeEvent?.Invoke(state);
    }
}