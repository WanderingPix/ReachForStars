using System.Linq;
using AmongUs.GameOptions;
using HarmonyLib;
using ReachForStars.Features.MainMenu;
using UnityEngine;

namespace ReachForStars.Patches;

[HarmonyPatch]
public class MainMenuPatches
{
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Awake))]
    [HarmonyPriority(Priority.Last)]
    [HarmonyPostfix]
    public static void OnMainMenuAwakePostfix(MainMenuManager __instance)
    {
        RFSLogo.Create(__instance);
        ReworkedMainMenu.SetUp(__instance);
    }
}