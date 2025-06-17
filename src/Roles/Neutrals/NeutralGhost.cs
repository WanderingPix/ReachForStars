using MiraAPI.Roles;
using UnityEngine;
using MiraAPI.GameEnd;
using MiraAPI.GameOptions;
using MiraAPI.Patches.Stubs;
using ReachForStars.Translation;
using MiraAPI.Modifiers;

namespace ReachForStars.Roles.Neutrals;

public class NeutralGhost : CrewmateGhostRole, ICustomRole
{
    public string RoleName => roleName.GetTranslatedText();
    public TranslationPool roleName = new TranslationPool(
        english: "Neutral Ghost",
        french: "Phantome Neutre",
        spanish: "Fantasma neutral",
        russian: "Нейтральный призрак",
        italian: ""
    );
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public TranslationPool RoleDescShort = new TranslationPool
    (
        english: "You are dead, watch the game unfold!",
        french: "Vous etes mort! regardez le reste de la partie",
        spanish: "¡Has muerto! ¡Siéntate y observa cómo se desarrolla el resto del juego!",
        russian: "Ты мёртв, смотри игру до конца.",
        italian: ""
    );
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Color.gray;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = false,
        CanGetKilled = true,
        CanUseVent = false,
        CanUseSabotage = false,
        TasksCountForProgress = false,
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
