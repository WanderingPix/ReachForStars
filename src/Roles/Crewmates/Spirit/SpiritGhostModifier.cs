using System.Collections;
using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using ReachForStars.Utilities;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Spirit;

public class SpiritGhostModifier : BaseModifier
{
    public override string ModifierName => "Spirit Ghost";
    private PlayerControl fakePlayer;
    public override void OnActivate()
    {
        fakePlayer = PlayerControlUtils.CreateFakePlayer(Player);
        Player.cosmetics.TogglePet(false);
        Player.Die(DeathReason.Exile, false);
        if (PlayerControl.LocalPlayer.Data.Role.IsImpostor) Player.Visible = true;
    }
    
    public override void OnDeactivate()
    {
        fakePlayer.gameObject.Destroy();
        Player.NetTransform.SnapTo(fakePlayer.transform.position);
        Player.cosmetics.TogglePet(true);
        Player.Revive();
    }

    public override void FixedUpdate()
    {
        if (Player.AmOwner == false) return;
        HudManager.Instance.Chat.chatButton.gameObject.SetActive(false);
    }
}