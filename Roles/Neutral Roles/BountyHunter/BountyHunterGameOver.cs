using HarmonyLib;
using MiraAPI.GameEnd;
using MiraAPI.Utilities;
using ReachForStars.Roles.Neutrals.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.BountyHunter;

public class BountyHunterWin : CustomGameOver
{
    public override bool VerifyCondition(PlayerControl playerControl, NetworkedPlayerInfo[] winners)
    {
        winners.AddItem<NetworkedPlayerInfo>(playerControl.Data);
        if (playerControl.Data.Role is BountyHunterRole)
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
        endGameManager.WinText.text = "The Jester has fooled the crew!";
        endGameManager.WinText.color = new Color(1f, 0.18f, 0.81f, 1f);
        endGameManager.BackgroundBar.material.SetColor(ShaderID.Color, new Color(1f, 0.18f, 0.81f, 1f));
    }
}
