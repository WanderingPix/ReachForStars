using HarmonyLib;
using ReachForStars.Features.Minimap;

namespace ReachForStars.Patches;

[HarmonyPatch]
public static class MapBehaviourPatches
{
    [HarmonyPatch(typeof(MapBehaviour), nameof(MapBehaviour.Show))]
    [HarmonyPostfix]
    public static void MapBehaviourSHowPostfix(MapBehaviour __instance)
    {
        RefreshedMapBehaviour.SetUp(__instance);
    }
}