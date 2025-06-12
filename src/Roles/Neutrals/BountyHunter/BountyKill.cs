using MiraAPI.Networking;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities;
using System.Collections;
using MiraAPI.Utilities;
using Reactor.Networking;
using MiraAPI.Roles;
using UnityEngine;
using System.Collections.Generic;
using System;
using Random = System.Random;
using ReachForStars.Features;
using ReachForStars.Roles.Neutrals.Roles;

namespace ReachForStars.Roles.Neutrals.BountyHunter;
public class BountyKill : CustomActionButton<PlayerControl>
{
    public override string Name => "kill";

    public override float Cooldown => 0;
    public override float EffectDuration => 0;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.RedKillButton;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is BountyHunterRole;
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance, false);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(CustomRoleSingleton<BountyHunterRole>.Instance.RoleColor));
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return PlayerControl.LocalPlayer.Data.Role is BountyHunterRole BH && BH.Target == target;
    }
    protected override void OnClick()
    {
        if (PlayerControl.LocalPlayer.Data.Role is BountyHunterRole BH)
        {
            PlayerControl.LocalPlayer.RpcCustomMurder(Target, true);
            HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.transform, 1.4f, 0.7f *SmolUI.ScaleFactor, 0.7f));
            BH.OnTargetKill();
        }   
    }

    public override void OnEffectEnd()
    {
    }
}
