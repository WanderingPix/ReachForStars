using MiraAPI.Roles;
using MiraAPI.Utilities.Assets;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Sheriff;

public class SheriffRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Sheriff";
    public string RoleLongDescription => "Choose who to shoot";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => Palette.White;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public Color OptionsMenuColor => Palette.CrewmateBlue;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        //Icon = Assets.SheriffIcon0,
    };
}