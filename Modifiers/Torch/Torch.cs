using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using UnityEngine;
using HarmonyLib;
using MiraAPI.Utilities;
using System;
using TheSillyRoles.Modifiers;

namespace ReachForStars.Addons.Torch;

public class TorchModifier : GameModifier
{
    public override string ModifierName => "Torch";

    public override int GetAmountPerGame()
    {
        return 15;
    }

    public override int GetAssignmentChance()
    {
        return 100;
    }
    public override void OnActivate()
    {
        PlayerControl.LocalPlayer.lightSource.flashlightSize = 3f;
        PlayerControl.LocalPlayer.lightSource.SetFlashlightEnabled(true);
    }
    public override void OnDeactivate()
    {
        PlayerControl.LocalPlayer.lightSource.SetFlashlightEnabled(false);
    }
}