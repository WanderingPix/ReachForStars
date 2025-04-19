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
            //Setup
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -5f);
            myRend = gameObject.AddComponent<SpriteRenderer>();
            myRend.sprite = Assets.FrozenBody0.LoadAsset();
            gameObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.45f);
            targetBody.gameObject.SetActive(false);

            //Collier doesn't work, dunno why
            myCollider = gameObject.AddComponent<BoxCollider>();
            myCollider.size = gameObject.transform.localScale * 1.2f;

            /passive button too
            myButton = gameObject.AddComponent<PassiveButton>();

            myButton.OnClick = new UnityEngine.UI.Button.ButtonClickedEvent();
            myButton.OnClick.AddListener(new Action(() =>
            {
                Hit();
            }));

            //Spawn Animation
            HudManager.Instance.StartCoroutine(Effects.Bounce(gameObject.transform, 0.7f, 0.45f));
            HudManager.Instance.StartCoroutine(Effects.ColorFade(myRend, new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f), 0.4f));
            HudManager.Instance.StartCoroutine(Effects.ScaleIn(gameObject.transform, 0.5f, 0.35f, 0.4f));
        }

        public void Hit()
        {
            durability-=1;
            HudManager.Instance.StartCoroutine(Effects.Bounce(gameObject.transform, 0.7f, 0.25f));
            DecreaseLevelByOne();
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
