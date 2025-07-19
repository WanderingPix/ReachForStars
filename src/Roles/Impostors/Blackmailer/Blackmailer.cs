using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Blackmailer;

public class BlackmailerRole : ImpostorRole, ICustomRole
{
    public TranslationPool RoleDescLong = new
    (
        "Silence crewmates!",
        "",
        "",
        ""
    );

    public TranslationPool RoleDescShort = new
    (
        "Silence players to stop them from revealing your secrets!",
        "",
        "",
        ""
    );

    public TranslationPool rolename = new
    (
        "Blackmailer",
        french: "",
        spanish: "",
        russian: ""
    );

    public override bool IsAffectedByComms => false;
    public string RoleName => rolename.GetTranslatedText();
    public string RoleDescription => RoleDescShort.GetTranslatedText();

    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = true,
        Icon = Assets.ChillerIcon,
        IntroSound = Assets.FreezeSFX,
        TasksCountForProgress = false
    };

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return GameManager.Instance.DidImpostorsWin(gameOverReason);
    }
}