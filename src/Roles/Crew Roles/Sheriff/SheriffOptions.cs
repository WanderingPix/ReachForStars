using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Roles;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Sheriff;

public class SheriffOptions : AbstractOptionGroup<SheriffRole>
{
    public override string GroupName => "Sheriff Options";
    [ModdedEnumOption("Misfire Consequence", typeof(MisfireResults))]
    public MisfireResults Consequence { get; set; } = MisfireResults.Demote;
    [ModdedNumberOption("Bullet Count", 1f, 3f, 1f, MiraAPI.Utilities.MiraNumberSuffixes.None)]
    public float BulletCount { get; set; } = 1f;
}
public enum MisfireResults
{
    Suicide,
    Demote,
    None
}
