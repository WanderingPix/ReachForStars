using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals;

public class NeutralWinner : GameModifier
{
    public TranslationPool hudString = new
    (
        "You have won! sit back watch the rest of the game unfold!",
        french: "Vous avez gagner! Regardez le reste de la partie!",
        spanish: "¡Has ganado! ¡Siéntate y observa cómo se desarrolla el resto del juego!",
        russian: "Вы выиграли! расслабьтесь и наблюдайте за ходом игры!"
    );

    public override string ModifierName => "Neutral Winner";
    public override bool HideOnUi => false;

    public override int GetAmountPerGame()
    {
        return 0;
    }

    public override int GetAssignmentChance()
    {
        return 0;
    }

    public override string GetDescription()
    {
        return hudString.GetTranslatedText();
    }

    public override void OnActivate()
    {
        if (Player == PlayerControl.LocalPlayer)
            Helpers.CreateTextLabel(hudString.GetTranslatedText(), HudManager.Instance.transform,
                AspectPosition.EdgeAlignments.Bottom, new Vector3(0f, 0f, 0f));
    }
}