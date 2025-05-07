using HarmonyLib;
using MiraAPI.GameEnd;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.Jester;

public class JesterWin : CustomGameOver
{
    public override bool VerifyCondition(PlayerControl playerControl, NetworkedPlayerInfo[] winners)
    {
        winners.AddItem<NetworkedPlayerInfo>(playerControl.Data);
        if (playerControl.Data.Role is JesterRole)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public TranslationPool EjectMessage = new TranslationPool
    (
        english: "The Jester has fooled the crew!",
        french: "Le Plaisantin a trollé l'équipage !",
        spanish: "¡El Bufón ha engañado a la tripulación!",
        portuguese: "O Bobo enganou a tripulação!",
        russian: "шут обхитрил экипаж!",
        italian: ""
    );

    public override void AfterEndGameSetup(EndGameManager endGameManager)
    {
        endGameManager.WinText.text = "The Jester has fooled the crew!";
        endGameManager.WinText.color = new Color(1f, 0.18f, 0.81f, 1f);
        endGameManager.BackgroundBar.material.SetColor(ShaderID.Color, new Color(1f, 0.18f, 0.81f, 1f));
    }
}
