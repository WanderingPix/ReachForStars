using HarmonyLib;
using ReachForStars.Features.Sabotages;

namespace ReachForStars
{
[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Die))]
public static class OnDeath
{
    public static void Prefix(PlayerControl __instance)
    {
        
        if (__instance == PlayerControl.LocalPlayer) DisableAnnoyingImpostorsSabotagingWhileDead.ToggleOffSabs();
    }
}
}
