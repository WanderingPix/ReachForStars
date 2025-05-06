using MiraAPI.Utilities.Assets;
using MiraAPI.Utilities;
using Reactor.Utilities.Extensions;
using Reactor.Utilities;
using System.Collections;
using System;
using Reactor.Utilities.Attributes;
using UnityEngine;
using Sentry.Unity.NativeUtils;

namespace ReachForStars.Roles.Impostors.Chiller;

[RegisterInIl2Cpp(typeof(IUsable))]
public class FrozenBody(IntPtr ptr) : MonoBehaviour(ptr)
{
    public bool isActive;
    public ImageNames UseIcon => ImageNames.UseButton;
    public float UsableDistance => 200f;
    public float PercentCool => 0;
    public CircleCollider2D myCol;
    public SpriteRenderer myRend;
    public DeadBody targetBody;

    int durability = 30;

    public void SetTargetBody(DeadBody body)
    {
        targetBody = body;
    }
    public void Start()
    {
        //Setup
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -5f);
        myRend = gameObject.AddComponent<SpriteRenderer>();
        myRend.sprite = Assets.FrozenBody0.LoadAsset();
        gameObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.45f);
        targetBody.gameObject.SetActive(false);
        myCol = gameObject.AddComponent<CircleCollider2D>();
        //myCol.bounds.size = gameObject.transform.localScale * 0.8f;



            //Spawn Animation
        HudManager.Instance.StartCoroutine(Effects.Bounce(gameObject.transform, 0.7f, 0.45f));
        HudManager.Instance.StartCoroutine(Effects.ColorFade(myRend, new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f), 0.4f));
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(gameObject.transform, 0.5f, 0.35f, 0.4f));
    }

    public void Use()
    {
        durability-=1;
        HudManager.Instance.StartCoroutine(Effects.Bounce(gameObject.transform, 0.7f, 0.25f));
        
        switch (durability)
        {
            case 30:
                myRend.sprite = Assets.FrozenBody0.LoadAsset();
                break;
            case 20:
                myRend.sprite = Assets.FrozenBody1.LoadAsset();
                break;
            case 10:
                myRend.sprite = Assets.FrozenBody2.LoadAsset();
                break;
            case 0:
                this.DestroyImmediate();
                break;
        }
    }
            
    
    public void OnDestroy()
    {
        targetBody.gameObject.SetActive(true);
        myRend.sprite = Assets.Puddle.LoadAsset();
    }

    /// <summary>
    ///     Updates the sprite outline for the consoles
    /// </summary>
    /// <param name="isVisible">TRUE iff the console is within vision</param>
    /// <param name="isTargeted">TRUE iff the console is the main target selected</param>
    public void SetOutline(bool on, bool mainTarget)
    {
        if (on) myRend.UpdateOutline(Palette.CrewmateBlue);
        else myRend.UpdateOutline(new Color(0f, 0f, 0f, 0f));
    }

    /// <summary
    ///     Checks whether or not the console is usable by a player
    /// </summary>
    /// <param name="playerInfo">Player to check</param>
    /// <param name="canUse">TRUE iff the player can access this console currently</param>
    /// <param name="couldUse">TRUE if the player could access this console in the future</param>
    /// <returns>Distance from console</returns>
    public float CanUse(NetworkedPlayerInfo pc, out bool canUse, out bool couldUse)
    {
        var num = float.MaxValue;
        var @object = pc.Object;
        couldUse = !pc.IsDead;
        canUse = couldUse;
        if (canUse)
        {
            var truePosition = @object.GetTruePosition();
            var position = transform.position;
            num = Vector2.Distance(truePosition, position);
            canUse &= num <= UsableDistance;
        }
        return num;
    }
}
