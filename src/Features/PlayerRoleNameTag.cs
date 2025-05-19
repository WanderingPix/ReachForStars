using System;
using HarmonyLib;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Roles;
using ReachForStars.Utilities;
using TMPro;
using MiraAPI.GameOptions;

namespace ReachForStars.Features
{
    public class RoleNameTag
    {
        /// <summary>
        /// Executed by Patches/OnCutsceneBreak.cs
        /// </summary>
        public static void SetRoleNameTag(RoleBehaviour role)
        {
            if (role.Player == PlayerControl.LocalPlayer && !OptionGroupSingleton<DevModeOptions>.Instance.ShowAllRoles)
            {
                PlayerControl.LocalPlayer.transform.GetChild(3).GetChild(0).GetComponent<TextMeshPro>().text = $"<size=2>{role.NiceName}</size>\n{PlayerControl.LocalPlayer.Data.PlayerName}\n\n";
            }
            else if (OptionGroupSingleton<DevModeOptions>.Instance.ShowAllRoles)
            {
                foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                {
                    p.transform.GetChild(3).GetChild(0).GetComponent<TextMeshPro>().text = $"<size=2>{p.Data.Role.NiceName}</size>\n{p.Data.PlayerName}\n\n";
                }
            }
        }
        
    }
}