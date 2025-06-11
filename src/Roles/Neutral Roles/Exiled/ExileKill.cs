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
using Rewired;

namespace ReachForStars.Roles.Neutrals.Exiled;
public class ExileKill : CustomActionButton<PlayerControl>
{
    public override string Name => "kill";

    public override float Cooldown => 1;
    public override float EffectDuration => 0;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.RedKillButton;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is Exiled;
    }
    public void OnEnable()
    {
        if (PlayerControl.LocalPlayer.Data.Role is Exiled exiled && exiled.EnemyTeam == ExiledEnemyTeam.Crewmates) Button.graphic.sprite = Assets.BlueKillButton.LoadAsset();
        else if (PlayerControl.LocalPlayer.Data.Role is Exiled exiled2 && exiled2.EnemyTeam == ExiledEnemyTeam.Impostors) Button.graphic.sprite = Assets.RedKillButton.LoadAsset();
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
            if(exiled.EnemyTeam == ExiledEnemyTeam.Impostors && Target.Data.Role.IsImpostor)
            {
                PlayerControl.LocalPlayer.RpcCustomMurder (Target, true);
                HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.transform, 1.4f, 0.7f, 0.7f));
            }
            else if (!Target.Data.Role.IsImpostor && exiled.EnemyTeam == ExiledEnemyTeam.Crewmates)
            {
                PlayerControl.LocalPlayer.RpcCustomMurder (Target, true);
                HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.transform, 1.4f, 0.7f, 0.7f));
            }
            else
            {
                HudManager.Instance.StartCoroutine(Effects.SwayX(Button.transform, 0.7f*SmolUI.ScaleFactor, 0.25f));
            }
        }   
    }

    public override void OnEffectEnd()
    {
    }
}
