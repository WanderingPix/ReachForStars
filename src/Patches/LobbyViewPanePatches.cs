using HarmonyLib;
using ReachForStars.Features;

namespace ReachForStars.Patches;

[HarmonyPatch]
public static class LobbyViewPanePatches
{
    [HarmonyPatch(typeof(LobbyInfoPane), nameof(LobbyInfoPane.RefreshPane))]
    [HarmonyPostfix]
    public static void LobbyInfoPaneRefreshPostfix(LobbyInfoPane __instance)
    {
        CollapsibleInfoPane.CreateButton(__instance);
    }
}