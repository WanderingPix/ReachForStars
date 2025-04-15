//using MiraAPI.Example.Options.Roles;
using MiraAPI.Example.Roles;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using TheSillyRoles.RPCHandler;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using System.Collections.Generic;
using UnityEngine.UI;
using ReachForStars.Utilities;
using Reactor.Utilities;

namespace ReachForStars.Roles.Impostors.Miner;
public class Dig : CustomActionButton
{
    public override string Name => "Dig";

    public override float Cooldown => 0;
    public override float EffectDuration => 5;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 1;

    public int VentCount = 0;

    public override LoadableAsset<Sprite> Sprite => Assets.PoisonButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is MinerRole;
    }

    public override bool CanUse()
    {
        return PlayerControl.LocalPlayer.CanPet() && UsesLeft != 0;
    }
    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.MyPhysics.enabled = false;
        
    }

    public override void OnEffectEnd()
    {
        PlayerControl.LocalPlayer.MyPhysics.enabled = true;
        PlayerControl.LocalPlayer.RpcPlaceVent(VentCount);        
        VentCount++;
    }
    
}