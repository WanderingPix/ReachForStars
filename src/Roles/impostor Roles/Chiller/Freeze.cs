using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ReachForStars.Roles.Impostors.Chiller;
public class Freeze : CustomActionButton<DeadBody>
{
    public override string Name => "Freeze";

    public override float Cooldown => 0;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 1;

    public override float Distance => 2f;


    public override LoadableAsset<Sprite> Sprite => Assets.Freeze;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is FreezerRole;
    }
    public override void SetOutline(bool active)
    {
        foreach (var rend in Target.bodyRenderers)
        {
            if (Target != null) rend.UpdateOutline(active ? Palette.ImpostorRed : null);
        }
    }
    public override DeadBody? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetNearestDeadBody(Distance);
    }
    public override bool CanClick()
    {
        return Target != null && UsesLeft > 0;
    }

    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcFreezeBody();
        SoundManager.Instance.PlaySound(Assets.FreezeSFX.LoadAsset(), false, 1f);
    }
}
