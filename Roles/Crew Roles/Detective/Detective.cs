using MiraAPI.Hud;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using Rewired;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Detective;


public class DetectiveRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Detective";
    public string RoleLongDescription => "PlaceHolder";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => Palette.White;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public List<PlayerControl> Suspects;
    public List<PlayerControl> ActualEvils;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
    };
    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);
        CustomButtonSingleton<Inspect>.Instance.Button.Show();

        Suspects = PlayerControl.AllPlayerControls.ToArray().ToList();
        ActualEvils = Suspects.Where(x => x.Data.Role.IsImpostor || x.Data.Role is ICustomRole custom && custom.Team == ModdedRoleTeams.Custom).ToList();
    }

}
