using MiraAPI.GameOptions;
using MiraAPI.GameOptions.OptionTypes;
using MiraAPI.Utilities;

namespace ReachForStars.Roles.Impostors.Stickster;

public class SticksterOptions : AbstractOptionGroup<SticksterRole>
{
    public override string GroupName => "Stickster Options";

    public ModdedNumberOption SlowedDownSpeed { get; } = new("Speed while in glue", 0.4f, 0.4f,
        0.6f, 0.1f, MiraNumberSuffixes.Multiplier);

    public ModdedToggleOption DoesGlueDespawn { get; } = new("Glue disappears after meetings", true);
}