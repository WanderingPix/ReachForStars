using MiraAPI.GameOptions;
using MiraAPI.GameOptions.OptionTypes;
using MiraAPI.Utilities;

namespace ReachForStars.Roles.Impostors.Witch;

public class WitchOptions : AbstractOptionGroup<WitchRole>
{
    public override string GroupName => "Witch Options";

    public ModdedNumberOption PoisonDelay { get; } = new("Time before pisoned player dies", 15f, 15f, 45f, 7.5f,
        MiraNumberSuffixes.Seconds);

    public ModdedToggleOption CanDoNormalKilling { get; } = new("Witch can do normal killing", true);
}