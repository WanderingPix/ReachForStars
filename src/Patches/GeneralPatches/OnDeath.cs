using ReachForStars.Addons.Flash;
using HarmonyLib;
using MiraAPI.Modifiers;
using ReachForStars.Features.Sabotages;

namespace ReachForStars
{
[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Die))]
public static class OnDeath
{
    public static void Prefix(this PlayerControl __instance)
    {
        DisableAnnoyingImpostorsSabotagingWhileDead.ToggleOffSabs();
    }
}
}
