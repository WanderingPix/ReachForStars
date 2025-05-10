using HarmonyLib;
using MiraAPI.GameOptions;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Modifiers;
using ReachForStars.Addons.Flash;
using ReachForStars.Addons.Grandpa;
using ReachForStars.Addons.Child;
using MiraAPI.Hud;
using ReachForStars.Roles.Impostors.Chiller;
using ReachForStars.Roles.Neutrals.Roles;

namespace ReachForStars
{
[HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    
    public static class OnKillButtonClick
    {
        public static void Postfix(KillButton __instance)
        {
            if (PlayerControl.LocalPlayer.Data.Role is BountyHunterRole BH)
            {
                BH.OnTargetKill();
            }
        }
    }
}