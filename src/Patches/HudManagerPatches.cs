using HarmonyLib;
using ReachForStars.Features;

namespace ReachForStars;

[HarmonyPatch]
public class HudManagerPatches
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    [HarmonyPostfix]
    public static void HudManagerStartPostfix(HudManager __instance)
    {
        SmolUI.ResizeUI();
    }

    [HarmonyPatch(typeof(ActionButton), nameof(ActionButton.Show))]
    [HarmonyPostfix]
    public static void AbilityButtonStartPostfix(ActionButton __instance)
    {
        RecolorableUsesCounter.SetUp(__instance);
    }
}