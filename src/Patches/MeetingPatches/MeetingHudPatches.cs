using System;
using System.Collections.Generic;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;
using System.Linq;
using ReachForStars.MeetingSettings;
using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;

namespace ReachForStars
{
[HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Awake))]
public class MeetingHudPatches
{
    public static void Postfix(MeetingHud __instance)
    {
        if (OptionGroupSingleton<MeetingOptions>.Instance.NoSkipping)
        {
        __instance.SkippedVoting.gameObject.DestroyImmediate();
        __instance.SkipVoteButton.gameObject.DestroyImmediate();
        }
    }
}
}