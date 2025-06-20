using HarmonyLib;

namespace ReachForStars;

[HarmonyPatch(typeof(RoleBehaviour))]
public class ShipStatusPatches
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    [HarmonyPostfix]
    public static void Postfix(ShipStatus __instance)
    {
        //TBD
    }
}