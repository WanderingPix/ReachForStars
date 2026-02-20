using MiraAPI.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Ninja;

public class NinjaRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Ninja";

    public string RoleDescription => "Throw shurikens.";

    public string RoleLongDescription => "Throw shurikens at players to kill them";

    public Color RoleColor => Palette.ImpostorRoleRed;

    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new(this);
}