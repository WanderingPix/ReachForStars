using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Roles;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.FogBringer;

public class FogBringerOptions : AbstractOptionGroup<FogBringerRole>
{
    public override string GroupName => "Fog Bringer Options";

    [ModdedToggleOption("Fog Clouds disappear after every round")]
    public bool DoFogCloudsDisappear { get; set; } = true;
}