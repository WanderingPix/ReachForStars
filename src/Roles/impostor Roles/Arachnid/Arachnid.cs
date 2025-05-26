using System.Collections.Generic;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Arachnid;


public class ArachnidRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Arachnid";
    public string RoleDescription => "Spiders are scawy -pix";
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

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return GameManager.Instance.DidImpostorsWin(gameOverReason);
    }
}
