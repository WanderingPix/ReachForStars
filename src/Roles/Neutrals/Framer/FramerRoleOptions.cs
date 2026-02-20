using MiraAPI.GameOptions;
using MiraAPI.GameOptions.OptionTypes;
using MiraAPI.Utilities;

namespace ReachForStars.Roles.Neutrals.Framer;

public class FramerRoleOptions : AbstractOptionGroup<FramerRole>
{
    public override string GroupName => "Framer Role Options";

    public ModdedNumberOption EjectedPlayersQuota { get; set; } = new("Successful Ejects Quota", 3, 1, 3, 1, MiraNumberSuffixes.None, "0 Ejects");
}