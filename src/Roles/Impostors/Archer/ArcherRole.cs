using MiraAPI.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Archer;

public class ArcherRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Archer";

    public string RoleDescription => "Bullseye!";

    public string RoleLongDescription => "Shoot arrows to kill players.";

    public Color RoleColor => Palette.ImpostorRoleRed;

    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new(this);
}