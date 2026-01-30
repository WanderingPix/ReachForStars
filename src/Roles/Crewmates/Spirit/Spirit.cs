using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Player;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Spirit;

public class SpiritRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Spirit";
    
    public string RoleDescription => "";
    
    public string RoleLongDescription => "";
    
    public Color RoleColor => RFSPalette.SpiritRoleColor;
    
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;
    
    public CustomRoleConfiguration Configuration => new(this); 
}