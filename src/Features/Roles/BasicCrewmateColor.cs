using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Gameplay;

namespace ReachForStars.Features;

public static class BasicCrewmateColor
{
    [RegisterEvent]
    public static void OnSetRole(SetRoleEvent @event)
    {
        var behaviour = RoleManager.Instance.GetRole(@event.Role);
        if (@event.Player == PlayerControl.LocalPlayer && @event.Player.Data.Role.TeamType == RoleTeamTypes.Crewmate)
            @event.Player.cosmetics.SetNameColor(Palette.White);
    }
}