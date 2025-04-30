using System;
using System.Collections.Generic;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TMPro;
using System.Linq;
using ReachForStars.MeetingSettings;
using AmongUs.GameOptions;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Reactor.Networking.Rpc;
using UnityEngine;
using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;

namespace TheSillyRoles
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    public class MeetingBreak
    {
        public static void Postfix()
        {
            if (OptionGroupSingleton<MeetingOptions>.Instance.MeetingBreakPatch == true)
            {
                ShipStatus.Instance.BreakEmergencyButton();
            }
        }
    }
}