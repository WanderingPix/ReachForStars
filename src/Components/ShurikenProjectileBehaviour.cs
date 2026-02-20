using System;
using System.Linq;
using MiraAPI.Networking;
using MiraAPI.Utilities;
using ReachForStars.Utilities;
using Reactor.Utilities;
using Reactor.Utilities.Attributes;
using Reactor.Utilities.Extensions;
using UnityEngine;

[RegisterInIl2Cpp]
public class ShurikenProjectileBehaviour(IntPtr ptr) : MonoBehaviour(ptr)
{
    public Vector2 vel;
    public PlayerControl source;
    public float lifetime;
    private void FixedUpdate()
    {
        transform.position += (Vector3) vel.normalized / 5;
        var p = Helpers.GetClosestPlayers(transform.position, 0.25f).First(x => x != source);
        if (p && source.AmOwner)
        {
            source.RpcCustomMurder(p, true, false, true, false, false);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime >= 3)
        {
            Destroy(gameObject);
        }
    }

    public static ShurikenProjectileBehaviour Create(PlayerControl source, Vector2 vel)
    {
        var arrow = UnityObject.Instantiate(ReachForStars.Assets.ShurikenProjectilePrefab.LoadAsset());
        arrow.layer = LayerMask.NameToLayer("Ship");
        arrow.transform.position = source.GetTruePosition();
        arrow.transform.localScale = new(0.5f, 0.5f, 1);
        var p = arrow.GetComponent<ShurikenProjectileBehaviour>();
        p.source = source;
        p.vel = vel;

        return p;
    }
}