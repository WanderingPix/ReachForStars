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
        french: "",
        spanish: "",
        russian: "",
        italian: ""
    );
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public TranslationPool RoleDescShort = new TranslationPool
    (
        english: "You have lost! Watch the game unfold as a ghost!",
        french: "Vous avez perdu! Regardez le reste de la partie!",
        spanish: "",
        russian: "",
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
