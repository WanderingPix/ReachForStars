using MiraAPI.Roles;
using UnityEngine;
using MiraAPI.Patches.Stubs;
using MiraAPI.Hud;
using Reactor.Localization.Utilities;
using MiraAPI.Utilities;
using MiraAPI.GameEnd;
using MiraAPI.Networking;
using MiraAPI.Events;
using Rewired;
using System.Linq;
using MiraAPI.Modifiers.Types;

namespace ReachForStars.Roles.Neutrals.CursedSoul;

public class PossessingNodifier : GameModifier
{
    public override string ModifierName => "Possessing";
    public override void OnActivate()
    {
        HudManager.Instance.KillButton.Show();
    }
    public override void OnDeactivate()
    {
        HudManager.Instance.KillButton.Hide();
    }


    public override int GetAmountPerGame()
    {
        return 0;
    }

    public override int GetAssignmentChance()
    {
        return 0;
    }
}