using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using UnityEngine;
using HarmonyLib;
using MiraAPI.Utilities;
using System;
using ReachForStars;

namespace ReachForStars.Addons;
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
    public override void OnActivate()
    {
        PlayerControl.LocalPlayer.RpcSetVisor("visor_king");
    }
    public override void OnDeactivate()
    {
        PlayerControl.LocalPlayer.RpcSetVisor("");
    }
}