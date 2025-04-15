using UnityEngine;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities.Extensions;
using Reactor.Utilities;
using System.Collections;
using System;

namespace ReachForStars.Roles.Impostors.Chiller
{
    public class FrozenBody : MonoBehaviour 
    {
        SpriteRenderer myRend;
        DeadBody targetBody;
        PassiveButton myButton;

        BoxCollider myCollider;

        int durability = 30;

        public void SetTargetBody(DeadBody body)
        {
            targetBody = body;
        }
        public void Start()
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -5f);
            HudManager.Instance.StartCoroutine(Effects.Bounce(gameObject.transform, 0.7f, 0.25f));

            myRend = gameObject.AddComponent<SpriteRenderer>();
            myRend.sprite = Assets.FrozenBody0.LoadAsset();
            gameObject.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
            targetBody.gameObject.SetActive(false);
            
            myCollider = gameObject.AddComponent<BoxCollider>();
            myCollider.size = gameObject.transform.localScale * 1.2f;

            myButton = gameObject.AddComponent<PassiveButton>();
            myButton.OnClick.AddListener(new Action(() =>
            {
                Hit();
            }));
        }

        public void Hit()
        {
            durability-=1;
            HudManager.Instance.StartCoroutine(Effects.Bounce(gameObject.transform, 0.7f, 0.25f));
            if (durability % 10 == 0)
            {
                DecreaseLevelByOne();
            }
        }
        public void DecreaseLevelByOne()
        {
            durability -= 10;
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
                    Break();
                    break;
            }
        }
        void FixedUpdate()
        {
            
        }
        public void Break()
        {
            targetBody.gameObject.SetActive(true);
            myRend.sprite = Assets.Puddle.LoadAsset();
            this.DestroyImmediate();
        }
    }
}