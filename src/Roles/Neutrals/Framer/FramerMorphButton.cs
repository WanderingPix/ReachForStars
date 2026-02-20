using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities.Assets;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.Framer;

public class FramerMorphButton : CustomActionButton
{
    protected override void OnClick()
    {
        var r = PlayerControl.LocalPlayer.Data.Role.TryCast<FramerRole>();
        if (r == null) return;
        PlayerControl.LocalPlayer.RpcAddModifier<FramerAbilitiesModifier>(r.Target);
    }

    public override void OnEffectEnd()
    {
        PlayerControl.LocalPlayer.CmdCheckRevertShapeshift(false);
    }

    public override bool Enabled(RoleBehaviour role)
    {
        return role is FramerRole;
    }

    public override string Name => "Morph";

    public override float Cooldown => 25;

    public override float EffectDuration => 15;

    public override LoadableAsset<Sprite> Sprite => Assets.PlaceHolder;
}