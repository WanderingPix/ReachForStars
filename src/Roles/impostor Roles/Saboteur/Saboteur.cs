using System.Collections.Generic;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Saboteur;


public class SaboteurRole : ImpostorGhostRole, ICustomRole
{
    public string RoleName => "Saboteur";
    public string RoleDescription => "Sabotage the ship while dead.";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        
    };

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override void Initialize(PlayerControl? playerControl)
    {
        RoleBehaviourStubs.Initialize(this, playerControl);
        HudManager.Instance.SabotageButton.Show();
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return GameManager.Instance.DidImpostorsWin(gameOverReason);
    }
}
