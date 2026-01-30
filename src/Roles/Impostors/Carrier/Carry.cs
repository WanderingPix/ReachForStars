using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using PowerTools;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Carrier;

public class Carry : CustomActionButton<DeadBody>
{
    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcAddModifier<CarryingModifier>(Target.ParentId);
    }

    public override bool Enabled(RoleBehaviour role)
    {
        return role is CarrierRole && !PlayerControl.LocalPlayer.HasModifier<CarryingModifier>();
    }
    public override string Name => "Carry";
    public override float Cooldown => 3;
    public override float Distance => 0.5f;
    public override LoadableAsset<Sprite> Sprite => Assets.PlaceHolder;
    public override DeadBody GetTarget()
    {
        return PlayerControl.LocalPlayer.GetNearestDeadBody(Distance);
    }

    public override bool IsTargetValid(DeadBody target)
    {
        return base.IsTargetValid(target) && !PlayerControl.LocalPlayer.HasModifier<CarryingModifier>();
    }

    public override void SetOutline(bool active)
    {
        //TODO
    }
}