using ReachForStars.Addons;
using HarmonyLib;
using MiraAPI.Modifiers;

namespace ReachForStars
{
[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcCompleteTask))]
public static class GrantBuffForTask
{
    public static void Prefix(this PlayerControl __instance)
    {
        if (__instance.GetModifierComponent().GetModifier<FlashModifier>() != null)
        {
            __instance.GetModifierComponent().GetModifier<FlashModifier>().IncreaseSpeed();
        }
    }
}
}
