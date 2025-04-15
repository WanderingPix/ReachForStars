using MiraAPI.Example.Roles;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using TheSillyRoles;
using Reactor.Utilities;
using System.Collections;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using TheSillyRoles.RPCHandler;
using TMPro;
using System.Threading;

namespace ReachForStars.Roles.Crewmates.Sheriff;
public class Shoot : CustomActionButton<PlayerControl>
{
    public override string Name => "Shoot";
    public override float Cooldown => 25;
    public override float EffectDuration => 0;

    public override int MaxUses => 1;

    public override LoadableAsset<Sprite> Sprite => Assets.Shoot;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is SheriffRole;
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance, false);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(new Color(1f, 1f, 0f, 1f)));
    }
    
    public override bool IsTargetValid(PlayerControl? target)
    {
        return true;
    }
    protected override void OnClick()
    {
        if (Target.Data.Role.IsImpostor)
        {
            PlayerControl.LocalPlayer.RpcCustomMurder(Target, true);
            Target.DeathReasonsRPC("You have been shot!!", 5);
        }
        else
        {
            
            Coroutines.Start(Demote());
        }
    }

    public override void OnEffectEnd()
    {
    }
    public IEnumerator Demote()
    {
        GameObject badge = new GameObject("Badge");
        badge.transform.position = new Vector3(0f, 1f, 0f);
        SpriteRenderer badgeRend = badge.AddComponent<SpriteRenderer>();

        badgeRend.sprite = Assets.SheriffIcon0.LoadAsset();
        yield return new WaitForSeconds(0.2f);

        badgeRend.sprite = Assets.SheriffIcon1.LoadAsset();
        yield return new WaitForSeconds(0.2f);

        badgeRend.sprite = Assets.SheriffIcon0.LoadAsset();
        yield return new WaitForSeconds(0.1f);

        badgeRend.sprite = Assets.SheriffIcon1.LoadAsset();
        yield return new WaitForSeconds(0.1f);

        badgeRend.sprite = Assets.SheriffIcon0.LoadAsset();
        yield return new WaitForSeconds(0.1f);

        badgeRend.sprite = Assets.SheriffIcon2.LoadAsset();

        yield break;
    }
}