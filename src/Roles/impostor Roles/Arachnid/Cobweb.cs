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

namespace ReachForStars.Roles.Impostors.Arachnid;

public class FrozenBody : MonoBehaviour(ptr)
{
    public BoxCollider2D myCol;
    public SpriteRenderer myRend;
  
    public void Start()
    {
        //Setup
        gameObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.45f);
        myCol = gameObject.AddComponent<BoxCollider2D>();
        myCol.size = gameObject.transform.localScale;
        myRend = gameObject.AddComponent<SpriteRenderer>();
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -5f);
        myRend.sprite = Assets.FrozenBody0.LoadAsset();
       
        //Spawn Animation
        HudManager.Instance.StartCoroutine(Effects.ColorFade(myRend, new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f), 0.4f));
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(gameObject.transform, 0f, 0.5f, 0.4f));
    }
    public void OnCollisionEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerControl>())
        {
            //TBD
        }
    }
}
