using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using UnityEngine;
using HarmonyLib;
using MiraAPI.Utilities;
using System;
using TheSillyRoles.Modifiers;

namespace TheSillyRoles.Modifiers;

public class DisorientedModifier : GameModifier
{
    public override string ModifierName => "Disoriented";

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
        HudManager.Instance.ToggleMapButton(false);
    }
    public override void OnDeactivate()
    {
        HudManager.Instance.ToggleMapButton(true);
    }
}