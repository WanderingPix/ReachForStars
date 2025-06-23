using System.Collections.Generic;
using MiraAPI.Hud;
using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Mole;

public class MoleRole : ImpostorRole, ICustomRole
{
    public TranslationPool rolename = new(
        "Mole",
        "Topo",
        "Taupe",
        "Моль"
    );

    public override bool IsAffectedByComms => false;
    public List<Vent> MinedVents { get; set; } = new();
    public string RoleName => rolename.GetTranslatedText();
    public string RoleDescription => "Throw vents around the map";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = true,
        Icon = Assets.DigButton,
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

    public override void OnMeetingStart()
    {
        CustomButtonSingleton<Dig>.Instance.SetUses(1);
    }
}