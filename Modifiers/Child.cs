using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using UnityEngine;
using HarmonyLib;
using ReachForStars.Addons;
using TheSillyRoles.RPCHandler;
using ReachForStars.Utilities;

namespace ReachForStars.Addons;

public class ChildModifier : GameModifier
{
    public override string ModifierName => "Child";

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
        PlayerControl.LocalPlayer.RpcResize(0.5f, 0.5f, 0.5f);
    }
    public override void OnDeactivate()
    {
        //TODO: Inverted effect 
    }
}