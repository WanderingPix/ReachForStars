using System;
using System.Collections.Generic;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;
using ReachForStars.Roles.Crewmates.Detective;
using Il2CppSystem;
using MiraAPI.Utilities;

namespace ReachForStars
{
[HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.PopulateButtons))]
public class MeetingHudPatches
{
    public static void Postfix(MeetingHud __instance)
    {
        if (PlayerControl.LocalPlayer.Data.Role is DetectiveRole det)
        {
            foreach (var area in __instance.GetComponentsInChildren<PlayerVoteArea>())
            {
                if (det.Suspects.Contains(area.GetPlayer()))
                {
                    var DetIndicator = Object.Instantiate<SpriteRenderer>(area.XMark, area.transform);
                    DetIndicator.sprite = Assets.Shoot.LoadAsset();
                    DetIndicator.gameObject.SetActive(true);
                }

            }
        }
    }
}
}