using MiraAPI.Roles;
using TMPro;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Detective;


public class DetectiveRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Detective";
    public string RoleLongDescription => "PlaceHolder";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => Palette.AcceptedGreen;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public List<PlayerControl> Suspects;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
    };
}
