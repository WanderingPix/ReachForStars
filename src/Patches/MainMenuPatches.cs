using HarmonyLib;
using ReachForStars.Features.MainMenu;

namespace ReachForStars.Patches;

[HarmonyPatch]
public class MainMenuPatches
{
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    [HarmonyPostfix]
    public static void OnMainMenuStartPostfix(MainMenuManager __instance)
    {
        Credits.CreateButton(__instance);
    }
}