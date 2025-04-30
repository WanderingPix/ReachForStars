using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using UnityEngine;

namespace ReachForStars.MiscSettings;

public class MiscOptions : AbstractOptionGroup
{
    public override string GroupName => "Misc Options";

    [ModdedToggleOption("Flip Map")]
    public bool FlippedMap { get; set; } = false;

    [ModdedToggleOption("Delete ALL vents")]
    public bool NoVents { get; set; } = true;
}
