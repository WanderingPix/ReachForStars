using System.Collections.Generic;
using MiraAPI.Hud;
using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Mole;

public class MoleRole : ImpostorRole, ICustomRole
{
    public TranslationPool rolename = new TranslationPool
    (
        english: "Mole",
        spanish: "Topo",
        french: "Taupe",
        russian: "Моль"
    );

    public override bool IsAffectedByComms => false;
    public List<Vent> MinedVents { get; set; } = new();
    public string RoleName => rolename.GetTranslatedText();
    public string RoleDescription => "Dig vents around the map";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = true,
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