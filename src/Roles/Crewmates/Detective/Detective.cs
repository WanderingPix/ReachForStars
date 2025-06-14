using MiraAPI.Hud;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using MiraAPI.Utilities;
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
    public Color OptionsMenuColor => Palette.CrewmateBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public List<PlayerControl> Suspects { get; set; } = new();
    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
    };
    public override void Initialize(PlayerControl player)
    {
        if (player == PlayerControl.LocalPlayer)
        {
            RoleBehaviourStubs.Initialize(this, player);
            CustomButtonSingleton<Inspect>.Instance.Button.Show();
        }
    }
    public void RegenerateSuspectList()
    {
        Suspects.Clear();
        foreach (PlayerControl p in Helpers.GetClosestPlayers(Player.GetTruePosition(), 6f, true).Where(x => x != Player && !x.Data.IsDead))
        {
            Suspects.Add(p);
        }
    }
    public void SetUpVoteArea(PlayerVoteArea area)
    {
        SpriteRenderer Indicator = Object.Instantiate<SpriteRenderer>(area.XMark, area.XMark.transform.parent);
        Indicator.gameObject.name = "DetectiveIndicator";
        Indicator.sprite = Assets.DetectiveIndicator.LoadAsset();
        Indicator.gameObject.SetActive(true);
    }
}
