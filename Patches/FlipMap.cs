using System;
using HarmonyLib;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Roles;
using ReachForStars.Utilities;
using MiraAPI.GameOptions;
using TMPro;
using ReachForStars.MiscSettings;

namespace ReachForStars
{
    public static class FlipMapPatch
    {
        /// <summary>
        /// Executed by Patches/OnCutsceneBreak.cs
        /// </summary>
        public static void TryFlipMap(this ShipStatus ship)
        {
            if (OptionGroupSingleton<MiscOptions>.Instance.FlippedMap)
            {
                Vector3 MapVec = ship.gameObject.transform.localScale;
                MapVec.x *= -1f;
            }
        }
        
    }
}