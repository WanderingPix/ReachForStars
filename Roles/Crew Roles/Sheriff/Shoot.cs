using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities;
using System.Collections;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using TMPro;

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
            HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.transform, 1.4f, 0.7f, 0.7f));
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
        GameObject badge = new GameObject("SheriffBadge");
        badge.transform.SetParent(HudManager.Instance.transform);
        AspectPosition pos = badge.AddComponent<AspectPosition>();
        pos.Alignment = AspectPosition.EdgeAlignments.Top;
        pos.DistanceFromEdge = new Vector3(0f, 1f, 0f);
        SpriteRenderer badgeRend = badge.AddComponent<SpriteRenderer>();

        badgeRend.sprite = Assets.SheriffIcon0.LoadAsset();
        yield return new WaitForSeconds(0.4f);

        badgeRend.sprite = Assets.SheriffIcon1.LoadAsset();
        yield return new WaitForSeconds(0.4f);

        badgeRend.sprite = Assets.SheriffIcon0.LoadAsset();
        yield return new WaitForSeconds(0.2f);

        badgeRend.sprite = Assets.SheriffIcon1.LoadAsset();
        yield return new WaitForSeconds(0.2f);

        badgeRend.sprite = Assets.SheriffIcon0.LoadAsset();
        yield return new WaitForSeconds(0.2f);

        badgeRend.sprite = Assets.SheriffIcon2.LoadAsset();
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(badge.transform, 1.6f, 1f, 0.7f));

        PlayerControl.LocalPlayer.RpcSetRole(AmongUs.GameOptions.RoleTypes.Crewmate, true);
        yield break;
    }
}
