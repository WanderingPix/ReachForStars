using Il2CppSystem;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Networking;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities.Extensions;
using Rewired;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.Framer;

public class FramerKillButton : CustomActionButton<PlayerControl>
{
    protected override void OnClick()
    {
        if (Target == null) return;
        PlayerControl.LocalPlayer.RpcCustomMurder(Target, true);
    }

    public override bool Enabled(RoleBehaviour role)
    {
        return PlayerControl.LocalPlayer.HasModifier<FramerAbilitiesModifier>();
    }

    public override string Name => "Kill";

    public override float Cooldown => 30;

    public override LoadableAsset<Sprite> Sprite => Assets.PlaceHolder;
    public override PlayerControl GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Nullable<Color>(RFSPalette.FramerRoleColor));
    }
}