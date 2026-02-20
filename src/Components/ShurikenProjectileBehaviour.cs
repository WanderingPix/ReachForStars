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
        if (!source.AmOwner && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, source.transform.position) < 0.1f)
        {
            PlayerControl.LocalPlayer.RpcCustomMurder(PlayerControl.LocalPlayer, true, false, true, false, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this);
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
            gameObject.Destroy();
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