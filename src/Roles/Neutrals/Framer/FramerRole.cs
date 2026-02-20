using System.Linq;
using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Meeting;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Networking;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using ReachForStars.Networking;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.Framer;

public class FramerRole : CrewmateRole, INeutralRole
{
    public string RoleName => "Framer";

    public string RoleDescription => "Frame players to get them voted out!";

    public string RoleLongDescription => "Morph into players, vent and kill to get them ejected!";

    public PlayerControl Target;

    public Color RoleColor => RFSPalette.FramerRoleColor;

    public CustomRoleConfiguration Configuration => new(this);

    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);
        if (Player.AmOwner)
        {
            CustomButtonSingleton<FramerMorphButton>.Instance.Button?.Show();
            Target = Helpers.GetAlivePlayers().Where(x => !x.AmOwner).Random();
        }
    }

    public int SuccessfulEjectsCount;
    
    [RegisterEvent]
    public static void OnMeetingComplete(EndMeetingEvent e)
    {
        if (PlayerControl.LocalPlayer.Data.Role is FramerRole r && e.MeetingHud.exiledPlayer == r.Target.Data)
        {
            r.Player.RpcSetBodyType(PlayerBodyTypes.Seeker);
            r.SuccessfulEjectsCount++;
            if (r.SuccessfulEjectsCount >= OptionGroupSingleton<FramerRoleOptions>.Instance.EjectedPlayersQuota)
            {
                r.Player.RpcAddModifier<NeutralWinner>();
                r.Player.RpcCustomMurder(r.Player, true, showKillAnim:false);
            }
        }
    }
}