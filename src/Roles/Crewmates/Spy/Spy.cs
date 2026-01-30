using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Player;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Spy;

public class SpyRole : CrewmateRole, ICustomRole
{
    public Color NameColor => Palette.White;
    public string RoleName => "Spy";
    public float currentCharge { get; set; } = OptionGroupSingleton<SpyOptions>.Instance.BatteryDuration.Value;
    public string RoleDescription => "Spy on players";
    public string RoleLongDescription => "Stalk Players to watch them";
    public Color RoleColor => RFSPalette.SpyRoleColor;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public CustomRoleConfiguration Configuration => new(this);

    public bool CanLocalPlayerSeeRole(PlayerControl player)
    {
        return false;
    }

    public override void OnMeetingStart()
    {
        if (Player != PlayerControl.LocalPlayer) return;
        
        var WatchBtn = CustomButtonSingleton<Watch>.Instance;
        WatchBtn.Target = null;
        WatchBtn.Button.Hide();
        
        CustomButtonSingleton<Stalk>.Instance.Button.Show();
    }

    public void Recharge()
    {
        currentCharge += OptionGroupSingleton<SpyOptions>.Instance.BatteryDuration.Value;
    }

    [RegisterEvent]
    public static void OnTaskComplete(CompleteTaskEvent e)
    {
        if (e.Player.Data.Role is SpyRole s)
        {
            s.Recharge();
        }
    }
}