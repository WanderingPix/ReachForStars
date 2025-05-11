using HarmonyLib;
using MiraAPI.GameEnd;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using ReachForStars.Roles.Neutrals.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.BountyHunter;

public class BountyHunterWin : CustomGameOver
{
    public override bool VerifyCondition(PlayerControl playerControl, NetworkedPlayerInfo[] winners)
    {
        winners.AddItem<NetworkedPlayerInfo>(playerControl.Data);
        if (playerControl.Data.Role is BountyHunterRole BH && BH.SuccessfulKills == 1)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public override void AfterEndGameSetup(EndGameManager endGameManager)
    {
        endGameManager.WinText.text = "\n<size=5>The Bounty Hunter has killed their targets!<size>";
        endGameManager.WinText.color = Color.white;
        endGameManager.BackgroundBar.material.SetColor(ShaderID.Color, CustomRoleSingleton<BountyHunterRole>.Instance.RoleColor);
    }
}
