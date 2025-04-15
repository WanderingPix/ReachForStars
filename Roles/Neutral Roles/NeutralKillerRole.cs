using MiraAPI.Roles;
using UnityEngine;

namespace MiraAPI.Example.Roles;

public class NeutralKiller : ImpostorRole, ICustomRole
{
    public string RoleName => "Placeholder gyat";
    public string RoleDescription => "Lurk in the shadows.";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Color.black;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = false,
    };

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return GameManager.Instance.DidHumansWin(gameOverReason);
    }
}
