using MiraAPI.Modifiers.Types;

namespace ReachForStars.Modifiers;

public class CanSeeGhostsModifier : GameModifier
{
    public override string ModifierName => "Can See Ghosts";
    public override bool HideOnUi => true;
    public override bool ShowInFreeplay => false;

    public override int GetAssignmentChance()
    {
        return 0;
    }

    public override int GetAmountPerGame()
    {
        return 0;
    }
}