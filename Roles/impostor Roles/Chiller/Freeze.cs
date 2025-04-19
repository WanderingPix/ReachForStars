using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ReachForStars.Roles.Impostors.Chiller;
public class Freeze : CustomActionButton
{
    public override string Name => "Freeze";

    public override float Cooldown => 0;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 1;

    public int VentCount = 0;

    public override LoadableAsset<Sprite> Sprite => Assets.PoisonButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is FreezerRole;
    }

    public override bool CanUse()
    {
        if (PlayerControl.LocalPlayer.GetNearestDeadBody(1.5f) != null)
        {
            return true;
        }
        else return false;
    }
    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcFreezeBody();
    }
}
