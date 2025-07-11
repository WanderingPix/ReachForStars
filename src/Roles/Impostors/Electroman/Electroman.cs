using MiraAPI.Hud;
using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Electroman;

public class ElectromanRole : ImpostorRole, ICustomRole
{
    public TranslationPool RoleDescLong = new(
        "Electrocute crewmates!",
        french: "",
        spanish: "",
        russian: ""
    );

    public TranslationPool RoleDescShort = new(
        "Electrocute crewmates!"
    );

    public TranslationPool roleName = new(
        "Electroman",
        french: "Eclaire"
    );

    public override bool IsAffectedByComms => false;
    public string RoleName => roleName.GetTranslatedText();
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();

    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        TasksCountForProgress = false,
        IntroSound = Assets.ElectromanIntroSfx
    };

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return GameManager.Instance.DidImpostorsWin(gameOverReason);
    }

    public override void OnMeetingStart()
    {
        CustomButtonSingleton<Electrocute>.Instance.Button.SetUsesRemaining(3);
    }
}