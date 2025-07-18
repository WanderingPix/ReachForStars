using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;

namespace ReachForStars.Roles.Neutrals.Jester;

public class JesterOptions : AbstractOptionGroup<JesterRole>
{
    public override string GroupName => "Jester Options";

    [ModdedToggleOption("Jester Can Vent")]
    public bool CanVent { get; set; } = true;

    [ModdedToggleOption("Jester Can Call Meeting")]
    public bool CanCallMeeting { get; set; } = true;
}