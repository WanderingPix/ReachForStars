using HarmonyLib;
using MiraAPI.GameEnd;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using ReachForStars.Roles.Neutrals.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.CursedSoul;

public class CUrsedSoulWin : CustomGameOver
{
    public override bool VerifyCondition(PlayerControl playerControl, NetworkedPlayerInfo[] winners)
    {
        winners.AddItem<NetworkedPlayerInfo>(playerControl.Data);
        if (playerControl.Data.Role is CursedSoulRole Cursed && !playerControl.Data.IsDead)
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
        endGameManager.WinText.text = "\n<size=4>Cursed Soul Wins!";
        endGameManager.WinText.color = Color.white;
        endGameManager.BackgroundBar.material.SetColor(ShaderID.Color, Color.white);
    }
}
