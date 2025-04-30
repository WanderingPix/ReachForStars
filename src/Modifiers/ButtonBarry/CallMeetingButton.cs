using System.Collections;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars;
using Reactor.Utilities;
using UnityEngine;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;

namespace ReachForStars.Addons.ButtonBarryButton;

public class CallMeeting : CustomActionButton
{
    public override string Name => "Call MEeting";

    public override float Cooldown => 30;

    public override float EffectDuration => 0;
    public override int MaxUses => 2;
    public override ButtonLocation Location => ButtonLocation.BottomLeft; 
    public override LoadableAsset<Sprite> Sprite => Assets.Shoot;

    public override bool Enabled(RoleBehaviour? role)
    {
        return PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.HasModifier<ButtonBarryAddon>();
    }

    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcStartMeeting(PlayerControl.LocalPlayer.Data);

        if (UsesLeft == 0 && PlayerControl.LocalPlayer.HasModifier<ButtonBarryAddon>())
        {
            PlayerControl.LocalPlayer.RpcRemoveModifier<ButtonBarryAddon>();
        }
    }
}
