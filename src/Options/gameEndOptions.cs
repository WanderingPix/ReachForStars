using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using UnityEngine;

namespace ReachForStars.GameEndSettings;

public class GameEndOptions : AbstractOptionGroup
{
    public override string GroupName => "game end options";

    [ModdedToggleOption("Tasks completion results in win")]
    public bool TaskWinToggle { get; set; } = true;

    [ModdedToggleOption("Neutrals stop game from ending")]
    public bool NeutralsStopGameEnd { get; set; } = false;
}
