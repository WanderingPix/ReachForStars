using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;

namespace ReachForStars.Roles.Impostors.Stickster
{
    public class SticksterOptions : AbstractOptionGroup<SticksterRole>
    {
        public override string GroupName => "Stickster Options";
        public ModdedNumberOption SlowedDownSpeed { get; } = new ModdedNumberOption("Speed while in glue", 0.4f, 0.4f, 0.6f, 0.1f, MiraAPI.Utilities.MiraNumberSuffixes.Multiplier);
    }
}