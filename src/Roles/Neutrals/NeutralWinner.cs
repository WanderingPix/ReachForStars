using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Neutrals
{
    public class NeutralWinner : GameModifier
    {
        public override string ModifierName => "Neutral Winner";

        public override int GetAmountPerGame()
        {
            return 0;
        }

        public override int GetAssignmentChance()
        {
            return 0;
        }
        public override bool HideOnUi => false;
        public override string GetHudString()
        {
            return hudString.GetTranslatedText();
        }
        public TranslationPool hudString = new
        (
            english: "You have won! sit back watch the rest of the game unfold!",
            french: "Vous avez gagner! Regardez le reste de la partie!",
            spanish: "¡Has ganado! ¡Siéntate y observa cómo se desarrolla el resto del juego!",
            russian: "Вы выиграли! расслабьтесь и наблюдайте за ходом игры!",
            italian: "Hai vinto! Divertiti a guardare la partita!"
        );
        public override void OnActivate()
        {
            if (Player == PlayerControl.LocalPlayer)
            {
                Helpers.CreateTextLabel(hudString.GetTranslatedText(), HudManager.Instance.transform, AspectPosition.EdgeAlignments.Bottom, new(0f, 1f, 0f));
            }
        }
    }
}
