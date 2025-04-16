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
using MiraAPI.Modifiers;
using ReachForStars.Addons.BombHolder;

namespace ReachForStars.Roles.Impostors.Striker;
public class Strap : CustomActionButton<PlayerControl>
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
        return role is StrikerRole;
    }
    public override void SetOutline(bool active)
    {
        Target.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 0f, 1f)));
    }

    public override PlayerControl GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(false, Distance);
    }

    protected override void OnClick()
    {
        Target.RpcAddModifier<Bomb>();
        
    }

    public override void OnEffectEnd()
    {
    } 
}