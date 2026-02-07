using System;
using MiraAPI.Networking;
using MiraAPI.Utilities;
using ReachForStars.Utilities;
using Reactor.Utilities;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace ReachForStars.Components;

[RegisterInIl2Cpp]
public class ArrowProjectileBehaviour(IntPtr ptr) : MonoBehaviour(ptr)
{
    public Vector2 vel;
    public PlayerControl source;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var touchingPlayer = collision.otherCollider.TryGetComponent(out PlayerControl p);
        if (touchingPlayer && p.AmOwner)
        {
            source.RpcCustomMurder(p, true, false, true, false, false);
        }
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ship"))
        {
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3) vel.normalized * Time.fixedDeltaTime; 
    }
}