using System.Collections.Generic;
using MiraAPI.Hud;
using MiraAPI.Roles;
using UnityEngine;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Impostors.Shadow;


public class ShadowRole : ImpostorRole, ICustomRole
{
    public string RoleName => rolename.GetTranslatedText();
    public TranslationPool rolename = new TranslationPool
    (
        english: "Darkness",
        spanish: "Topo",
        french: "Taupe",
        italian: "Talpa",
        russian: ""
    );
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
    }
}
