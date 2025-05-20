using ServerHint.Components;
using HarmonyLib;
using LabApi.Features.Wrappers;

namespace ServerHint.Patches
{

    [HarmonyPatch(typeof(Player), nameof(Player.SendHint), typeof(string), typeof(float))]
    internal static class ShowHint
    {
        private static bool Prefix(Player __instance, string message, float duration = 3f)
        {
            HudManager.ShowHint(__instance, message, duration);
            return false;
        }
    }
}