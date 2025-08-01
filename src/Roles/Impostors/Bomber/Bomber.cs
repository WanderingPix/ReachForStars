using MiraAPI.Hud;
using MiraAPI.PluginLoading;
using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Bomber;

[MiraIgnore] //TODO: Finish this role
public class BomberRole : ImpostorRole, ICustomRole
{
    public TranslationPool rolename = new(
        "Bomber",
        "",
        "",
        ""
    );

    public override bool IsAffectedByComms => false;
    public string RoleName => rolename.GetTranslatedText();
    public string RoleDescription => "Place vents around the map";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = true,
        Icon = Assets.DigButton,
        TasksCountForProgress = false,
        IntroSound = Assets.DigSfx
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
        CustomButtonSingleton<BombAbility>.Instance.SetUses(1);
    }
}