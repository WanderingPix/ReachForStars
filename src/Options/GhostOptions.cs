using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace ReachForStars;

public class GhostOptions : AbstractOptionGroup
{
    public override string GroupName => "Ghost Options";

    [ModdedToggleOption("Ghosts see death popups")]
    public bool MeetingBreakPatch { get; set; } = false;

    [ModdedToggleOption("Disable Skipping")]
    public bool NoSkipping { get; set; } = false;
}