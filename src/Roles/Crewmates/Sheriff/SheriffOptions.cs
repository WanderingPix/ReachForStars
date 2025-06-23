using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;

namespace ReachForStars.Roles.Crewmates.Sheriff;

public sealed class SheriffOptions : AbstractOptionGroup<SheriffRole>
{
    public override string GroupName => "Sheriff Options";

    [ModdedEnumOption("Misfire Consequence", typeof(MisfireResults))]
    public MisfireResults Consequence { get; set; } = MisfireResults.Demote;

    [ModdedNumberOption("Bullet Count", 1f, 3f)]
    public float BulletCount { get; set; } = 1f;
}

public enum MisfireResults
{
    Suicide,
    Demote,
    None
}