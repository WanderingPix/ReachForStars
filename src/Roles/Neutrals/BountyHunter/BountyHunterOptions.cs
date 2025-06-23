using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using ReachForStars.Roles.Neutrals.Roles;

namespace ReachForStars.Roles.Neutrals.BountyHunter;

public sealed class BountyHunterOptions : AbstractOptionGroup<BountyHunterRole>
{
    public override string GroupName => "Bounty Hunter Options";

    [ModdedNumberOption("Required Assassinations Count", 3f, 6f)]
    public float SuccessfulKillsQuota { get; set; } = 3f;
}