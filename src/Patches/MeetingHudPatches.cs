using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarmonyLib;
using MiraAPI.Utilities;
using ReachForStars.Roles.Crewmates.Detective;
using UnityEngine;

namespace ReachForStars.Patches
{
    [HarmonyPatch(typeof(MeetingHud))]
    public class MeetingHudPatches
    {
        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.PopulateButtons))]
        [HarmonyPostfix]
        public static void PopulatePostfix(MeetingHud __instance)
        {
            if (PlayerControl.LocalPlayer.Data.Role is DetectiveRole det)
            {
                foreach (PlayerVoteArea area in UnityEngine.Object.FindObjectsOfType<PlayerVoteArea>())
                {
                    if (det.Suspects.Contains(area.GetPlayer()))
                    {
                        SpriteRenderer DetIndicator = UnityEngine.Object.Instantiate<SpriteRenderer>(area.XMark, area.XMark.transform.parent);
                        DetIndicator.gameObject.SetActive(true);
                        DetIndicator.sprite = Assets.Shoot.LoadAsset();
                        DetIndicator.gameObject.name = "DetIndicator";
                    }
                }
            }
        }
    }
}