using MiraAPI.GameOptions;
using MiraAPI.GameOptions.OptionTypes;
using MiraAPI.Utilities;

namespace ReachForStars.Modifiers.Game.Universal.Lonely;

public class LonelyModifierOptions : AbstractOptionGroup<LonelyModifier>
{
    public override string GroupName => "Lonely Modifier Options";

    public override bool ShowInModifiersMenu => true;

    public ModdedNumberOption AmountPerGame { get; set; } =
        new ModdedNumberOption("Amount per Game", 0, 0, 15, 1, MiraNumberSuffixes.None);

    public ModdedNumberOption AssignmentChance { get; set; } = new ModdedNumberOption("Assignment Chance", 0, 0, 100,
        10, "0", "0", MiraNumberSuffixes.Percent, "0", true);
}