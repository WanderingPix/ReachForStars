using MiraAPI.Modifiers.Types;

namespace ReachForStars.Modifiers;

public class InvisibleModifier : GameModifier
{
    public override string ModifierName => "Invis";
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

    public override void OnDeactivate()
    {
        Player.Visible = true;
    }
}