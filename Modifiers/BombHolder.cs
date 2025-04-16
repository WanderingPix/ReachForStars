using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using UnityEngine;
using TheSillyRoles.RPCHandler;
using ReachForStars.Utilities;
using System.Linq;
using MiraAPI.Networking;
using ReachForStars.Roles.Impostors.Witch;
using Reactor.Utilities;
using System.Collections;
using Reactor.Utilities.Extensions;

namespace ReachForStars.Addons.BombHolder;

public class Bomb : TimedModifier
{
    public override string ModifierName => "Bomb";

    public override float Duration => 30f;

    public GameObject? BombBadge;
    public SpriteRenderer? BombBadgeRend;
    public AspectPosition? BombBadgePos;

    public override void OnMeetingStart()
    {
        PlayerControl.LocalPlayer.RpcCustomMurder(PlayerControl.LocalPlayer, true);
        Player.RpcRemoveModifier<Bomb>();
    }
    public void FixedUpdate()
    {
        if (TimeRemaining == 3)
        {
            BombBadge = new GameObject("BombBadge");
            BombBadgeRend = BombBadge.AddComponent<SpriteRenderer>();
            BombBadgePos = BombBadge.AddComponent<AspectPosition>();
            BombBadgePos.Alignment = AspectPosition.EdgeAlignments.Center;
            BombBadgePos.DistanceFromEdge = new Vector3(0f, -1, 0f);

            Coroutines.Start(BombAnim(BombBadge.transform, BombBadgeRend));
        }
    }
    public IEnumerator BombAnim(Transform target, SpriteRenderer rend)
    {

        BombBadgeRend.sprite = Assets.Puddle.LoadAsset();
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(target, 1.4f, 0.7f, 1f));
        HudManager.Instance.StartCoroutine(Effects.SwayX(target, 3f, 0.25f));

        yield return new WaitForSeconds(1f);

        HudManager.Instance.StartCoroutine(Effects.ScaleIn(target, 0.7f, 1.2f, 2f));

        yield return new WaitForSeconds(1f);
        BombBadgeRend.material.color = Color.white;
        //Kill method
        target.gameObject.DestroyImmediate();
        yield break;
    }
}