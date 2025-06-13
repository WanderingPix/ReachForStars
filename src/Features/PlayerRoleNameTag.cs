using System;
using HarmonyLib;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Roles;
using ReachForStars.Utilities;
using TMPro;
using MiraAPI.GameOptions;
using ReachForStars.Roles;
using ReachForStars.Translation;

namespace ReachForStars.Features
{
    public class RoleNameTag
    {
        /// <summary>
        /// Executed by Patches/OnCutsceneBreak.cs
        /// </summary>
        public static void SetRoleNameTag(RoleBehaviour role)
        {
            if (role.Player == PlayerControl.LocalPlayer)
            {
                if (role is not ICustomRole) PlayerControl.LocalPlayer.transform.GetChild(3).GetChild(0).GetComponent<TextMeshPro>().text = $"<size=2>{role.NiceName}</size>\n{PlayerControl.LocalPlayer.Data.PlayerName}\n\n";
                else if (role is ICustomRole custom && custom.CanLocalPlayerSeeRole(PlayerControl.LocalPlayer)) PlayerControl.LocalPlayer.transform.GetChild(3).GetChild(0).GetComponent<TextMeshPro>().text = $"<size=2>{custom.RoleName}</size>\n{PlayerControl.LocalPlayer.Data.PlayerName}\n\n";

                if (role is ICustomRole custom1 && custom1.Team == ModdedRoleTeams.Crewmate) role.Player.cosmetics.nameText.color = Palette.White;
            }
        }
        
    }
}