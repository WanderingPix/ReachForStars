using System;
using HarmonyLib;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Roles;
using ReachForStars.Utilities;
using ReachForStars.MiscSettings;
using ReachForStars.Roles.CursedSoul;
using MiraAPI.Networking;
using Reactor.Utilities.Extensions;
using MiraAPI.GameOptions;
using ReachForStars.GameEndSettings;
using System.Linq;

namespace ReachForStars
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public class OnHudStart
    {
        /// Heavy rewrite of CheckEndCriteria
        public static void Postfix(HudManager __instance)
        {
            foreach (Transform trans in __instance.GetComponentsInChildren<Transform>())
            {
                trans.transform.localScale *= 0.5f; //Once the client settings pr is merged, add a slider for this
            }
        }
    }
}