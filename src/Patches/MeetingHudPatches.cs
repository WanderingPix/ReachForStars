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
        [HarmonyPatch(typeof(PlayerVoteArea), nameof(PlayerVoteArea.Start))]
        [HarmonyPostfix]
        public static void PlayerVoteAreaStartPostfix(PlayerVoteArea __instance)
        {
            if (PlayerControl.LocalPlayer.Data.Role is DetectiveRole det && det.Suspects.Contains(__instance.GetPlayer()))
            {
                det.SetUpVoteArea(__instance);
            }
        }
    }
}