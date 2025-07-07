using MiraAPI.GameOptions;
using MiraAPI.GameOptions.OptionTypes;

namespace ReachForStars.Roles.Impostors.Electroman;

public class ElectromanOptions : AbstractOptionGroup<ElectromanRole>
{
    public override string GroupName => "Witch Options";

    public ModdedToggleOption CanDoNormalKilling { get; } = new("Electroman can do normal killing", true);
}