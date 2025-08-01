using MiraAPI.GameOptions;
using MiraAPI.GameOptions.OptionTypes;
using MiraAPI.Utilities;
using ReachForStars.Roles.Neutrals.Roles.GhostBuster;

namespace ReachForStars.Roles.Neutrals.GhostBuster;

public class GhostBusterOptions : AbstractOptionGroup<GhostBusterRole>
{
    public override string GroupName => "Ghost Buster Options";

    public ModdedNumberOption GhostsQuota { get; set; } = new("Ghosts Required To Catch", 3, 3,
        PlayerControl.AllPlayerControls.Count, 1, MiraNumberSuffixes.None);
}