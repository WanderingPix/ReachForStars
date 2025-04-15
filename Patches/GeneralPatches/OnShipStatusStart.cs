using System;
using HarmonyLib;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Roles;
using ReachForStars.Utilities;
using ReachForStars.MiscSettings;
using ReachForStars.Roles.CursedSoul;
using MiraAPI.Networking;

namespace ReachForStars
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    public class OnShipStatusStart
    {
        public static void Postfix(ShipStatus __instance)
        {
            NoVents.TryBreakVent();
            RoleNameTag.SetRoleNameTag();
            
            __instance.TryFlipMap();
        }
    }
}