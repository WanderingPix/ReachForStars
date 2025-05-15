using AmongUs.GameOptions;
using HarmonyLib;
using ReachForStars.Features;

namespace ReachForStars
{
    [HarmonyPatch(typeof(PlayerControl))]
    public class PlayerCntrolPatches
    {
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Start))]
        [HarmonyPostfix]
        public static void PostfixStart(PlayerControl __instance)
        {
            __instance.gameObject.AddComponent<ExtendedPlayerControl>().parent = __instance;
        }
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CoSetRole))]
        [HarmonyPostfix]
        public static void PostfixCoSetRole(PlayerControl __instance, ref RoleTypes role)
        {
            __instance.GetExtendedPlayerControl().RoleHistory.Add(role);
        }
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
        [HarmonyPostfix]
        public static void PostfixMurderPlayer(PlayerControl __instance, ref PlayerControl target)
        {
            target.GetExtendedPlayerControl().Killer = __instance;
            target.GetExtendedPlayerControl().deathReason = DeathReason.Kill;
        }
    }
}
