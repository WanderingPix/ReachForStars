using MiraAPI.GameOptions;
using MiraAPI.GameOptions.OptionTypes;

namespace ReachForStars.Roles.Crewmates.Sheriff;

public class SheriffOptions : AbstractOptionGroup<SheriffRole>
{
    public override string GroupName => "Sheriff Options";

    public ModdedToggleOption SheriffKnowsIfRight { get; set; } = new("Sheriff knows if they shot the impostor", true);
}