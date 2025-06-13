using System;
using MiraAPI.GameOptions;
using MiraAPI.Utilities;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ReachForStars.Roles.Neutrals.BountyHunter
{
    public class BountyHud : MonoBehaviour
    {
        public SpriteRenderer myRend;
        public TextMeshPro Counter;
        public TextMeshPro WantedText;
        public AspectPosition myPos;
        public PoolablePlayer myPlayer;
        public PassiveButton myButton;
        public Animator myAnim;
        public bool IsOpen;
        public void ToggleHud(bool Show)
        {
            if (Show)
            {
                myAnim.runtimeAnimatorController = Assets.BountyHudOpenAnimController.LoadAsset();
            }
            else if (!Show)
            {
                
                myAnim.runtimeAnimatorController = Assets.BountyHudCloseAnimController.LoadAsset();
            }
            myPlayer.gameObject.SetActive(Show);
            Counter.gameObject.SetActive(Show);
            WantedText.gameObject.SetActive(Show);
            IsOpen = Show;
        }
        public void Start()
        {
            Counter = gameObject.transform.GetChild(2).GetComponent<TextMeshPro>();
            Counter.text = "";
            for (int i = 0; i != (int)OptionGroupSingleton<BountyHunterOptions>.Instance.SuccessfulKillsQuota; i++)
            {
                Counter.text = $"{Counter.text}<sprite=6>";
            }

            WantedText = gameObject.transform.GetChild(1).GetComponent<TextMeshPro>();

            myRend = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

            myPos = gameObject.AddComponent<AspectPosition>();
            myPos.Alignment = AspectPosition.EdgeAlignments.Top;
            myPos.DistanceFromEdge = new Vector3(0f, 1.5f, 0f);
            myPos.AdjustPosition();

            gameObject.transform.localPosition = new(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0f);

            myAnim = myRend.gameObject.AddComponent<Animator>();

            IsOpen = true;

            myButton = myRend.gameObject.AddComponent<PassiveButton>();
            var Event = new Button.ButtonClickedEvent();
            Event.AddListener(OnClick());
            myButton.OnClick = Event;
        }
        public Action OnClick()
        {
            void Listener()
            {
                PluginSingleton<ReachForStars>.Instance.Log.LogDebug("User has clicked the bounty hud! closing ui...");
                ToggleHud(!IsOpen);
            }

            return Listener;
        }


        public void OnNewTargetGenerated(NetworkedPlayerInfo info)
        {
            if (myPlayer) myPlayer.gameObject.DestroyImmediate();
            //set up PoolablePlayer
            myPlayer = HudManager.Instance.IntroPrefab.CreatePlayer(1, 1, info, false);
            myPlayer.transform.SetParent(gameObject.transform);
            myPlayer.transform.localPosition = new Vector3(0f, -0.15f, 0f);
            foreach (var rend in myPlayer.gameObject.GetComponentsInChildren<SpriteRenderer>(true))
            {
                rend.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
            HudManager.Instance.StartCoroutine(Effects.ScaleIn(gameObject.transform, 1.2f, 1f, 0.7f));
        }
        public void UpdateCount(int count)
        {
            Counter.text = "";
            int i;
            for (i = 0; i != count; i++) //Killed Bounties
            {
                Counter.text = $"{Counter.text}<sprite=5>";
            }
            for (int r = 0; r != (int)OptionGroupSingleton<BountyHunterOptions>.Instance.SuccessfulKillsQuota - count; r++) //remaining bounties
            {
                Counter.text = $"{Counter.text}<sprite=6>";
            }
        }
    }
}