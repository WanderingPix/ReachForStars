using MiraAPI.Utilities.Assets;
using MiraAPI.Utilities;
using Reactor.Utilities.Extensions;
using Reactor.Utilities;
using System.Collections;
using System;
using Reactor.Utilities.Attributes;
using UnityEngine;
using Sentry.Unity.NativeUtils;
using ReachForStars.Networking;
using System.Linq;
using MiraAPI.Modifiers;

namespace ReachForStars.Roles.Impostors.Arachnid;

public class Cobweb : MonoBehaviour
{
    public SpriteRenderer myRend;

    public bool ShouldAffectPlayer(PlayerControl Player)
    {
        return Player.Data.Role is not ArachnidRole && !Player.Data.IsDead;
    }
    public void Start()
    {
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        myRend = gameObject.AddComponent<SpriteRenderer>();
        Coroutines.Start(DoSpawnAnimation(myRend));
    }
    public IEnumerator DoSpawnAnimation(SpriteRenderer rend)
    {
        rend.sprite = Assets.Cobweb0.LoadAsset();
        yield return new WaitForSeconds(0.1f);

        rend.sprite = Assets.Cobweb1.LoadAsset();
        yield return new WaitForSeconds(0.1f);

        rend.sprite = Assets.Cobweb2.LoadAsset();
        yield return new WaitForSeconds(0.1f); 

        yield break;
    }

    public void FixedUpdate()
    {
        foreach (var player in PlayerControl.AllPlayerControls)
        {
            if (ShouldAffectPlayer(player) && Vector2.Distance(player.GetTruePosition(), transform.position) < 0.7f)
            {
                player.AddModifier<SlowedDownModifier>();
            }
        }
    }
}
