using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;
using MiraAPI.Utilities;

namespace ReachForStars.Roles.Neutrals.Pirate;

public class PirateOptions : AbstractOptionGroup<PirateRole>
{
    public override string GroupName => "Pirate Options";

    public ModdedNumberOption GoldPerTask =
        new("Gold Per Task Completed", 25, 50, 75, 25, MiraNumberSuffixes.None, "0 Gold");

    public ModdedNumberOption MurderChanceWhenStealing = new("Murder chance when stealing", 40, 0, 100, 10, "0", "0", MiraNumberSuffixes.Percent, "0 Gold", halfIncrements:true);
}