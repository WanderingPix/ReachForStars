using MiraAPI.Modifiers.Types;

namespace ReachForStars.Roles.Crewmates.Jailor;

public class JaileeModifier : GameModifier
{
    public PlayerControl Jailor;
    public override string ModifierName { get; } = "Jailed";

    public override int GetAssignmentChance()
    {
        return 0;
    }

    public override int GetAmountPerGame()
    {
        return 0;
    }

    public override void OnMeetingStart()
    {
    }
}