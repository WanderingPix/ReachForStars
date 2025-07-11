using MiraAPI.GameOptions;
using MiraAPI.GameOptions.OptionTypes;
using MiraAPI.Utilities;

namespace ReachForStars.Roles.Impostors.Electroman;

public class ElectromanOptions : AbstractOptionGroup<ElectromanRole>
{
    public override string GroupName => "Electroman Options";

    public ModdedNumberOption Distance { get; set; } =
        new("Electric Shock Distance", 1.5f, 1.5f, 3f, .5f, MiraNumberSuffixes.Multiplier);
}