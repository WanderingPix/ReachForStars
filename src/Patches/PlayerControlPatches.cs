using HarmonyLib;
using MiraAPI.Modifiers;
using ReachForStars.Modifiers;

namespace ReachForStars.Patches;

[HarmonyPatch]
public class PlayerControlPatches
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    [HarmonyPostfix]
    public static void Postfix(PlayerControl __instance)
    {
        if (__instance.HasModifier<InvisibleModifier>()) __instance.Visible = false;
    }
}