using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using System.Linq;
using MiraAPI.PluginLoading;

namespace ReachForStars.Roles.Neutrals.CursedSoul;
public class PossessButton : CustomActionButton<DeadBody>
{
    public override string Name => "Possess";

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

    public override bool IsTargetValid(DeadBody? target)
    {
        return true;
    }

    public override void SetOutline(bool active)
    {
        if (Target != null)
        {
            foreach (var rend in Target.bodyRenderers)
            {
                rend.UpdateOutline(active ? Color.gray : null);
            }
        }
    }


    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcReviveFromBody(true, Target.ParentId);
        Button.Hide();
    }
}
