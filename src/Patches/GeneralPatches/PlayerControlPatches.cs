using AmongUs.GameOptions;
using HarmonyLib;
using MiraAPI.Events;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using ReachForStars.Features;
using ReachForStars.Roles.Neutrals.Roles;

namespace ReachForStars
{
    [HarmonyPatch(typeof(PlayerControl))]
    public class PlayerCntrolPatches
    {
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Start))]
        [HarmonyPostfix]
        public static void PostfixStart(PlayerControl __instance)
        {
            ExtendedPlayerControl exp = __instance.gameObject.AddComponent<ExtendedPlayerControl>();
            exp.parent = __instance;
            
        }
        /*[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CoSetRole))]
        [HarmonyPostfix]
        public static void PostfixCoSetRole(PlayerControl __instance, ref RoleTypes role)
        {
            __instance.GetExtendedPlayerControl().RoleHistory.Add(role);
        }*/
        [RegisterEvent]
        public static void PostfixMurderPlayer(MiraAPI.Events.Vanilla.Gameplay.AfterMurderEvent @event)
        {
            @event.Target.GetExtendedPlayerControl().Killer = @event.Source;
            @event.Target.GetExtendedPlayerControl().deathReason = DeathReason.Kill;

            if (@event.Source.Data.Role is BountyHunterRole BH && BH.BountyTarget == @event.Target)
            {
                BH.OnTargetKill();
            }
        }
    }
}
