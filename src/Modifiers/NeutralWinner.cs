using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using ReachForStars.Utilities;

namespace ReachForStars.Addons.NeutralWinner;

public class NeutralWinner : GameModifier
{
    public override string ModifierName => "NeutralWinner";

    public override int GetAmountPerGame()
    {
        return 0;
    }

    public override int GetAssignmentChance()
    {
        return 0;
    }
}
