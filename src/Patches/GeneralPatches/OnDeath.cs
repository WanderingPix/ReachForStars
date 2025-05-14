using System;
using HarmonyLib;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using ReachForStars.Features.Sabotages;
using ReachForStars.Roles.Impostors.Manipulator;
using ReachForStars.Roles.Neutrals.Roles;

namespace ReachForStars
{
[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
public static class OnDeath
{
    public static void Prefix(PlayerControl __instance, ref PlayerControl target)
    {
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
