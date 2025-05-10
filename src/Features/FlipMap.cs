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
                ship.gameObject.transform.localScale = new Vector3(ship.gameObject.transform.localScale.x*-1f, ship.gameObject.transform.localScale.y, ship.gameObject.transform.localScale.z);
            }
        }
        
    }
}
