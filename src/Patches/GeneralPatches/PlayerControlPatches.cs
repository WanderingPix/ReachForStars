using AmongUs.GameOptions;
using HarmonyLib;
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
            __instance.gameObject.AddComponent<ExtendedPlayerControl>().parent = __instance;
        }
        /*[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CoSetRole))]
        [HarmonyPostfix]
        public static void PostfixCoSetRole(PlayerControl __instance, ref RoleTypes role)
        {
            __instance.GetExtendedPlayerControl().RoleHistory.Add(role);
        }*/
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
        [HarmonyPostfix]
        public static void PostfixMurderPlayer(PlayerControl __instance, ref PlayerControl target)
        {
            target.GetExtendedPlayerControl().Killer = __instance;
            target.GetExtendedPlayerControl().deathReason = DeathReason.Kill;

            if (PlayerControl.LocalPlayer.Data.Role is BountyHunterRole BH && __instance == PlayerControl.LocalPlayer && target == BH.BountyTarget)
            {
                BH.OnTargetKill();
            }


            if (PlayerControl.LocalPlayer.Data.Role is ManipulatorRole Manip && __instance == CustomButtonSingleton<Manipulate>.Instance.CurrentlyManipulatedPlayer)
            {
                Manipulate manipulatebtn = CustomButtonSingleton<Manipulate>.Instance;
                PlayerControl.LocalPlayer.RpcRemoveModifier<ManipulatedModifier>();
                manipulatebtn.overlay.ShowKillAnimation(PlayerControl.LocalPlayer.Data, manipulatebtn.CurrentlyManipulatedPlayer.Data);
            }
        }
    }
}
