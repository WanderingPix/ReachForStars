using HarmonyLib;
using MiraAPI.LocalSettings;
using ReachForStars.Features.Freeplay;
using ReachForStars.Options;

namespace ReachForStars.Patches;

[HarmonyPatch]
public class ShipStatusPatches
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    [HarmonyPostfix]
    public static void ShipStatusStartPostfix(ShipStatus __instance)
    {
        FreeplayOptionsLaptop.Create();
    }
}