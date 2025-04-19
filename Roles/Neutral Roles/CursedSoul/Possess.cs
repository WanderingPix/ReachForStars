using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using System.Linq;
using ReachForStars.Roles.Neutrals.CursedSoul;

namespace ReachForStars.Buttons.CursedSoul;
public class posess : CustomActionButton<DeadBody>
{
    public override string Name => "posess";

    public override float Cooldown => 5;
    public override float EffectDuration => 5;

    public override ButtonLocation Location => ButtonLocation.BottomLeft;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.PoisonButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is CursedSoulRole && PlayerControl.LocalPlayer.Data.IsDead;
    }

    public override DeadBody? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetNearestDeadBody(Distance);
    }

    public override void SetOutline(bool active)
    {
    }

    public override bool IsTargetValid(DeadBody? target)
    {
        return true;
    }
    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.Revive();
        foreach (var p in PlayerControl.AllPlayerControls)
        {
            if (p.Data.PlayerId == Target.ParentId)
            {
                PlayerControl.LocalPlayer.RpcShapeshift(p, false);
            }
        }
    }
}
