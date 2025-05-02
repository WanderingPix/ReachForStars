using Il2CppSystem.Web.Util;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Shadow;


public class ShadowRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Shadow";
    public string RoleDescription => "Lurk in the shadows.";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = false,
        CanGetKilled = true,
        CanUseVent = true,
    };

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }
    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return GameManager.Instance.DidImpostorsWin(gameOverReason);
    }
}
