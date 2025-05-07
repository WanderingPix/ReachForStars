using MiraAPI.GameOptions;
using MiraAPI.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Witch;


public class WitchRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Witch";
    public string RoleDescription => "Lurk in the shadows.";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = OptionGroupSingleton<WitchOptions>.Instance.CanDoNormalKilling.Value,
        CanGetKilled = true,
        CanUseVent = OptionGroupSingleton<WitchOptions>.Instance.Canvent.Value,
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
