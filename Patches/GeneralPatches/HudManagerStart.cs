using System;
using HarmonyLib;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Roles;
using ReachForStars.Utilities;
using ReachForStars.MiscSettings;
using ReachForStars.Roles;
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
        public static void Postfix(HudManager __instance)
        {
            foreach (SpriteRenderer rend in __instance.GetComponentsInChildren<SpriteRenderer>(true))
            {
                rend.gameObject.transform.localScale *= 1f; //Once chipseq's client settings pr is merged, add a slider for this 
            }
        }
    }
}
