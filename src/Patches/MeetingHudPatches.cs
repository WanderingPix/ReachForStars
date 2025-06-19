using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarmonyLib;
using Il2CppSystem;
using MiraAPI.Utilities;
using ReachForStars.Roles.Crewmates.Detective;
using ReachForStars.Utilities;
using UnityEngine;

namespace ReachForStars.Patches
{
    [HarmonyPatch]
    public static class MeetingHudPatches
    {
        [HarmonyPatch(typeof(PlayerVoteArea), nameof(PlayerVoteArea.SetCosmetics))]
        [HarmonyPostfix]
        public static void PlayerVoteAreaSetCosmeticsPostfix(PlayerVoteArea __instance, ref NetworkedPlayerInfo playerInfo)
        {
            byte targetId = playerInfo.PlayerId;
            if (PlayerControl.LocalPlayer.Data.Role is DetectiveRole det && det.Suspects.Where(x => x.PlayerId == targetId).Count() > 0)
            {
                det.SetUpVoteArea(__instance); 
            }
        }
    }
}