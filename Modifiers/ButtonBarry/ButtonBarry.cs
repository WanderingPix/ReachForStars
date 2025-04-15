using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using UnityEngine;
using HarmonyLib;
using TheSillyRoles;
using MiraAPI.Hud;
using ReachForStars.Addons.ButtonBarryButton;

namespace ReachForStars.Addons;

public class ButtonBarryAddon : GameModifier
{
    public override string ModifierName => "Button Barry";

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
        CustomButtonSingleton<CallMeeting>.Instance.Button.Show();
    }
}