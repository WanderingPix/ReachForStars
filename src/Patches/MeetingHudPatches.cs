using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarmonyLib;
using MiraAPI.Utilities;
using ReachForStars.Roles.Crewmates.Detective;
using ReachForStars.Utilities;
using UnityEngine;

namespace ReachForStars.Patches
{
    [HarmonyPatch]
    public class MeetingHudPatches
    {
        [HarmonyPatch(typeof(PlayerVoteArea), nameof(PlayerVoteArea.SetCosmetics))]
        [HarmonyPostfix]
        public static void SetCosmeticsPostfix(PlayerVoteArea __instance, ref NetworkedPlayerInfo pInfo)
        {
            if (PlayerControl.LocalPlayer.Data.Role is DetectiveRole det && PlayerControlUtils.GetPlayerById(pInfo.PlayerId))
            {
                det.SetUpVoteArea(__instance);
            }
        }
    }
}