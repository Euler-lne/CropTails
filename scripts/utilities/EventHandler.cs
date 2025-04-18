using System;

public class PlayerEvent
{
    public static Action<Enums.State> playerStateChangeEvent;
    public static void OnPlayerStateChangeEvent(Enums.State state)
    {
        playerStateChangeEvent?.Invoke(state);
    }
}