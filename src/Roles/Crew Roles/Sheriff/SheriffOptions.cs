using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Roles;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Sheriff;

public class SheriffOptions : AbstractOptionGroup
{
    public override string GroupName => "Sheriff Options";
    [ModdedEnumOption("Misfire Consequence", typeof(MisfireResults))]
    public MisfireResults Consequence { get; set; } = MisfireResults.Demote;
}
public enum MisfireResults
{
    Suicide,
    Demote,
    None
}
