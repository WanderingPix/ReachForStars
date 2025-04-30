using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace ReachForStars.Addons.Torch;

public class TorchOptions : AbstractOptionGroup
{
    public override string GroupName => "Torch Settings";

    public override bool ShowInModifiersMenu => true;

        [ModdedNumberOption("Torch size", 1f, 5f, 0.5f)]
    public float TorchSize { get; set; } = 3f;

    [ModdedNumberOption("TOrch chance", 0f, 100f, 10f)]
    public float Percentage { get; set; } = 0f;

    [ModdedNumberOption("Torch size", 0f, 15f, 1f)]
    public float TorchCount { get; set; } = 0f;

}
