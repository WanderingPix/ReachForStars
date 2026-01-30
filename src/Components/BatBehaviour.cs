using System;
using MiraAPI.Networking;
using MiraAPI.Utilities;
using ReachForStars.Utilities;
using Reactor.Utilities;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace ReachForStars.Components;
[RegisterInIl2Cpp]
public class BatBehaviour(IntPtr ptr) : MonoBehaviour(ptr)
{
    public Vector3 vel;
    public PlayerControl sourcePlayer;
    public SpriteRenderer spriteRenderer;
    public float lifetime;
    public float duration;
    public bool canKill = true;
    public bool canMove = true;
    private void FixedUpdate()
    {
        duration += Time.fixedDeltaTime;
        if (duration >= lifetime)
        {
            Despawn();
        }

        if (canMove)
        {
            Vector3 newPos = transform.position + vel * Time.deltaTime;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }

        if (canKill)
        {
            foreach (var p in Helpers.GetClosestPlayers(transform.position, 0.1f, true))
            {
                if (sourcePlayer.AmOwner) sourcePlayer.RpcCustomMurder(p, teleportMurderer:false, showKillAnim:false, resetKillTimer:false);
            }
        }
    }

    public void Despawn()
    {
        canKill = false;
        canMove = false;
        
        sourcePlayer.StartCoroutine(Effects.Slide2DWorld(transform, transform.position, transform.position + new Vector3(10, 100, 0), 1));
        Coroutines.Start(RFSEffects.ColorFadeAndDestroy(spriteRenderer, Color.white, Color.clear, 1.2f));
    }
}