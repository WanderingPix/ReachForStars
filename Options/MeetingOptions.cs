using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace ReachForStars.MeetingSettings;

public class MeetingOptions : AbstractOptionGroup
{
    public override string GroupName => "Meeting Options";

    [ModdedToggleOption("Disable Emergency meeting button")]
    public bool MeetingBreakPatch { get; set; } = false;

    [ModdedToggleOption("Disable Skipping")]
    public bool NoSkipping { get; set; } = false;
}

public enum BestApi
{
    MiraAPI,
    Mitochondria,
    Reactor,
}
