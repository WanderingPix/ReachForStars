using MiraAPI.Roles;
using TMPro;
using UnityEngine;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Crewmates.Trapper;


public class TrapperRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Trapper";
    public string RoleLongDescription => "ja ja ja you have not win";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => Palette.White;
    public Color OptionsMenuColor => Palette.CrewmateBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;
    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        //OptionsScreenshot = ExampleAssets.Banner,
    }; 
}
