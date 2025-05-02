using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using UnityEngine;
using HarmonyLib;
using MiraAPI.Utilities;
using System;
using ReachForStars;

namespace ReachForStars.Addons.Grandpa;
public class WujModifier : GameModifier
{
    public override string ModifierName => "Grandpa";

    public override int GetAmountPerGame()
    {
        return 15;
    }

    public override int GetAssignmentChance()
    {
        return 100;
    }

    public string prevVisor = "";
    public override void OnActivate()
    {
    }
    public override void OnDeactivate()
    {
    }
}
