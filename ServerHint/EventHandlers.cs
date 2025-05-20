using ServerHint.Components;
using LabApi.Events.Arguments.PlayerEvents;

namespace ServerHint
{
    public static class EventHandlers
    {
        public static void OnLeft(PlayerLeftEventArgs ev)
        {
            if (ev.Player.GameObject.TryGetComponent(out HudManager hudManager))
                hudManager.Destroy();
        }
        public static void OnJoined(PlayerJoinedEventArgs ev)
        {
            ev.Player.GameObject.AddComponent<HudManager>();
        }
    }
}