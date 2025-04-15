 //using MiraAPI.Example.Options.Roles;
using MiraAPI.Example.Roles;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using TheSillyRoles;
using Reactor.Utilities;
using System.Collections;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using Reactor.Networking.Rpc;
using Reactor.Networking;
using MiraAPI.Roles;
using UnityEngine;
using System.Collections.Generic;
using System;
using Random = System.Random;
using System.Threading.Tasks;

namespace ReachForStars.Roles.Neutrals.Exiled;
public class ExileKill : CustomActionButton<PlayerControl>
{
    public override string Name => "kill";

    public override float Cooldown => 1;
    public override float EffectDuration => 0;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.ShadowKillButton;

    public override ButtonLocation Location => ButtonLocation.BottomLeft;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is Exiled;
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance, false);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(CustomRoleSingleton<Exiled>.Instance.RoleColor));
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return true;
    }
    protected override void OnClick()
    {
        if (PlayerControl.LocalPlayer.Data.Role is Exiled exiled)
        {
            if(exiled.EnemyTeam == "impostors" && Target.Data.Role.IsImpostor)
            {
                PlayerControl.LocalPlayer.RpcCustomMurder (Target, true);
                HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.transform, 0.9f, 0.7f, 0.7f));
            }
            else if (!Target.Data.Role.IsImpostor && exiled.EnemyTeam == "crewmates")
            {
                PlayerControl.LocalPlayer.RpcCustomMurder (Target, true);
                HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.transform, 1.2f, 0.7f, 0.7f));
            }
            else
            {
                HudManager.Instance.StartCoroutine(Effects.SwayX(Button.transform, 0.7f, 0.25f));
            }
        }   
    }

    public override void OnEffectEnd()
    {
    }
}