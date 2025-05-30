using System;

public class PlayerEvent
{
    public static Action<Enums.State> playerStateChangeEvent;
    public static void OnPlayerStateChangeEvent(Enums.State state)
    {
        playerStateChangeEvent?.Invoke(state);
    }
}

public class DamageEvent
{
    public static Action<bool> getHurtEvent;
    public static void OnGetHurtEvent(bool isDead)
    {
        getHurtEvent?.Invoke(isDead);
    }
}