using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities.Extensions;
using Rewired;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.Framer;

public class FramerVentButton : CustomActionButton<Vent>
{
    protected override void OnClick()
    {
        if (Target == null) return;
        if (PlayerControl.LocalPlayer.inVent)
        {
            PlayerControl.LocalPlayer.MyPhysics.RpcExitVent(Target.Id);
            Target.SetButtons(false);
        }
        else
        {
            PlayerControl.LocalPlayer.MyPhysics.RpcEnterVent(Target.Id);
            Target.SetButtons(true);
        }
    }

    public override bool CanUse()
    {
        return base.CanUse() || PlayerControl.LocalPlayer.inVent;
    }

    public override bool Enabled(RoleBehaviour role)
    {
        return PlayerControl.LocalPlayer.HasModifier<FramerAbilitiesModifier>();
    }

    public override string Name => "Vent";

    public override float Cooldown => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.PlaceHolder;
    public override Vent GetTarget()
    {
        return PlayerControl.LocalPlayer.GetNearestObjectOfType<Vent>(Distance, new ContactFilter2D().NoFilter());
    }

    public override void SetOutline(bool active)
    {
        Target?.myRend.SetOutline(RFSPalette.FramerRoleColor);
    }
}