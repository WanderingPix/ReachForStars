using System;
using HarmonyLib;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Roles;
using ReachForStars.Utilities;
using TMPro;

namespace ReachForStars.Features
{
    public class RoleNameTag
    {
        /// <summary>
        /// Executed by Patches/OnCutsceneBreak.cs
        /// </summary>
        public static void SetRoleNameTag(RoleBehaviour role)
        {
            if (role is ICustomRole customRole && role.Player == PlayerControl.LocalPlayer)
            {
                PlayerControl.LocalPlayer.transform.GetChild(3).GetChild(0).GetComponent<TextMeshPro>().text = $"<size=2>{customRole.RoleName}</size>\n{PlayerControl.LocalPlayer.Data.PlayerName}\n\n";
            }
            else if (role is RoleBehaviour vanillarole && vanillarole.Player == PlayerControl.LocalPlayer)
            {
                PlayerControl.LocalPlayer.transform.GetChild(3).GetChild(0).GetComponent<TextMeshPro>().text = $"<size=2>{vanillarole.NiceName}</size>\n{PlayerControl.LocalPlayer.Data.PlayerName}\n\n";
            }
        }
        
    }
}