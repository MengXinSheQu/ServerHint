using ServerHint.Components;
using ServerHint.Enums;
using LabApi.Features.Wrappers;

namespace ServerHint
{
    public static class Extensions
    {
        public static void ShowManagedHint(this Player player, string message, float duration = 3f, bool overrideQueue = true, DisplayLocation displayLocation = DisplayLocation.MiddleBottom) =>
            HudManager.ShowHint(player, message, duration, overrideQueue, displayLocation);
    }
}