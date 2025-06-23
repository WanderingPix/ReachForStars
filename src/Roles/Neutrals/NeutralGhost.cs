using MiraAPI.Modifiers;
using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals;

public class NeutralGhost : CrewmateGhostRole, ICustomRole
{
    public TranslationPool RoleDescShort = new(
        "You are dead, watch the game unfold!",
        french: "Vous etes mort! regardez le reste de la partie",
        spanish: "¡Has muerto! ¡Siéntate y observa cómo se desarrolla el resto del juego!",
        russian: "Ты мёртв, смотри игру до конца."
        //italian: "Sei morto, osserva il resto della partita"//haha frenchie, I stole your idea
    );

    public TranslationPool roleName = new(
        "Neutral Ghost",
        french: "Phantome Neutre",
        spanish: "Fantasma neutral",
        russian: "Нейтральный призрак"
        //italian: "Spirito Neutrale"
    );

    public string RoleName => roleName.GetTranslatedText();
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Color.gray;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new(this)
    {
        UseVanillaKillButton = false,
        CanGetKilled = true,
        CanUseVent = false,
        CanUseSabotage = false,
        TasksCountForProgress = false
    };

    public virtual bool CanLocalPlayerSeeRole(PlayerControl player)
    {
        return false;
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return Player.HasModifier<NeutralWinner>();
    }
}