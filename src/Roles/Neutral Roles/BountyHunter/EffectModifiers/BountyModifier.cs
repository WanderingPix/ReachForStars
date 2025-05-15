using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using UnityEngine;
using MiraAPI.Networking;
using MiraAPI.GameOptions;
using MiraAPI.Roles;
using TMPro;
using Il2CppSystem.Web.Util;
using Reactor.Utilities;
using System.Collections;
using Hazel;
using Reactor.Utilities.Extensions;
using MiraAPI.Hud;
using ReachForStars.Roles.Neutrals.Roles;

namespace ReachForStars.Roles.Neutrals.BountyHunter;

public class BountyModifier : TimedModifier
{
    public override string ModifierName => "Target";
    public override float Duration => 25f; //TODO make this a config option

    public override void OnMeetingStart()
    {
        Player.RpcRemoveModifier<BountyModifier>();
    }
    public override void OnDeath(DeathReason reason)
    {
        if (PlayerControl.LocalPlayer.Data.Role is BountyHunterRole BH && BH.BountyTarget == Player && reason is DeathReason.Kill)
        {
            BH.OnTargetKill();
        }
    }
    public override bool HideOnUi => true;
}
