using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Gameplay;

namespace ReachForStars.Features.Roles;

public static class BasicCrewmateColor
{
    [RegisterEvent]
    public static void OnSetRole(SetRoleEvent @event)
    {
        var behaviour = RoleManager.Instance.GetRole(@event.Role);
        if (@event.Player == PlayerControl.LocalPlayer && behaviour.TryCast<CrewmateRole>() != null)
            @event.Player.cosmetics.SetNameColor(Palette.White);
    }
}