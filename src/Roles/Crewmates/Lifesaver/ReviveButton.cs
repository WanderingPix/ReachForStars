using MiraAPI.Hud;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Components;
using ReachForStars.Networking;
using ReachForStars.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Lifesaver;

public class ReviveButton : CustomActionButton<DeadBody>
{
    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcRevive(PlayerControlUtils.GetPlayerById(Target.ParentId));
    }

    public override bool Enabled(RoleBehaviour role)
    {
        return role is LifesaverRole { hasFirstAidKit: true };
    }

    public override string Name => "First Aid";

    public override float Cooldown => 40; //TODO Opt

    public override LoadableAsset<Sprite> Sprite => Assets.ReviveButton;

    public override DeadBody GetTarget()
    {
        return PlayerControl.LocalPlayer.GetNearestDeadBody(Distance);
    }

    public override void SetOutline(bool active)
    {
        Target?.bodyRenderers[0].SetOutline(active ? RFSPalette.LifesaverRoleColor : Color.clear);
    }
}