using HarmonyLib;
using ReachForStars.Roles.Impostors.Electroman;

namespace ReachForStars.Patches;

[HarmonyPatch]
public static class ConsolePatches
{
    [HarmonyPatch(typeof(Console), nameof(Console.Use))]
    [HarmonyPatch(typeof(MapConsole), nameof(MapConsole.Use))]
    [HarmonyPrefix]
    public static bool ConsoleUsePrefix(Console __instance)
    {
        var shortcircuit = __instance.GetComponent<ShortCircuitedConsole>();
        if (shortcircuit && !PlayerControl.LocalPlayer.Data.IsDead)
        {
            shortcircuit.Use();
            return false;
        }

        return true;
    }
}