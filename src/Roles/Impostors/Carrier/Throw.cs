using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Carrier;

public class ThrowBody : CustomActionButton
{
    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcRemoveModifier<CarryingModifier>();
    }

    public override bool Enabled(RoleBehaviour role)
    {
        return role is CarrierRole && PlayerControl.LocalPlayer.HasModifier<CarryingModifier>();
    }
    public override string Name => "Throw";
    public override float Cooldown => 0;
    public override LoadableAsset<Sprite> Sprite => Assets.PlaceHolder;
}