using HarmonyLib;
using ReachForStars.Features.GameOverScreen;

namespace ReachForStars.Patches;

[HarmonyPatch]
public static class EndGameManagerPatches
{
    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
    [HarmonyPrefix]
    public static void Prefix(EndGameManager __instance)
    {
        ShowRolesOnEndGameScreen.CacheRoles();
    }

    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
    [HarmonyPostfix]
    public static void Postfix(EndGameManager __instance)
    {
        ShowRolesOnEndGameScreen.SetUp(__instance);
    }
}