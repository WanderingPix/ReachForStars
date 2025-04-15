using System;
using System.Collections.Generic;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;
using System.Linq;
using MiraAPI.Modifiers;
using ReachForStars.Addons;

namespace ReachForStars
{
public class PooledPlayerPatches
{
    [HarmonyPatch(typeof(PoolablePlayer), nameof(PoolablePlayer.Awake))]
    public static void Postfix(ref NetworkedPlayerInfo pData, PoolablePlayer __instance)
    {
        foreach (var playercontrol in PlayerControl.AllPlayerControls)
        {
            if (playercontrol.Data.PlayerId == pData.PlayerId && playercontrol.HasModifier<ChildModifier>())
            {
                playercontrol.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }
}
} 
