using System;
using HarmonyLib;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Roles;
using ReachForStars.Utilities;
using TMPro;

namespace ReachForStars
{
    public class RoleNameTag
    {
        /// <summary>
        /// Executed by Patches/OnCutsceneBreak.cs
        /// </summary>
        public static void SetRoleNameTag()
        {
            if (PlayerControl.LocalPlayer.Data.Role is ICustomRole customRole)
            {
                PlayerControl.LocalPlayer.transform.GetChild(3).GetChild(0).GetComponent<TextMeshPro>().text = $"<size=2>{customRole.RoleName}</size>\n{PlayerControl.LocalPlayer.Data.PlayerName}\n\n";
            }
            else if (PlayerControl.LocalPlayer.Data.Role is RoleBehaviour rolebehaviour)
            {
                PlayerControl.LocalPlayer.transform.GetChild(3).GetChild(0).GetComponent<TextMeshPro>().text = $"<size=2>{rolebehaviour.NiceName}</size>\n{PlayerControl.LocalPlayer.Data.PlayerName}\n\n";
            }
        }
        
    }
}