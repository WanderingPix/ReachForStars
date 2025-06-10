using System;
using MiraAPI.GameOptions;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.BountyHunter
{
    public class BountyHud : MonoBehaviour
    {
        public SpriteRenderer myRend;
        public TextMeshPro Counter;
        public AspectPosition myPos;
        public PoolablePlayer myPlayer;
        public PassiveButton myButton;
        public Animator myAnim;
        public bool IsOpen;
        public void ToggleHud(bool Show)
        {
            if (Show)
            {
                myPlayer.gameObject.SetActive(Show);
                myAnim.runtimeAnimatorController = Assets.BountyHudOpenAnimController.LoadAsset();
            }
            else if (!Show)
            {
                myPlayer.gameObject.SetActive(Show);
                myAnim.runtimeAnimatorController = Assets.BountyHudCloseAnimController.LoadAsset();
            }
            IsOpen = Show;
        }
        public void Start()
        {
            Counter = gameObject.transform.GetChild(2).GetComponent<TextMeshPro>();
            myRend = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            myPos = gameObject.AddComponent<AspectPosition>();
            myPos.Alignment = AspectPosition.EdgeAlignments.Top;
            myPos.DistanceFromEdge = new Vector3(0f, 1.25f, 0f);
            myPos.AdjustPosition();
            myAnim = gameObject.AddComponent<Animator>();

            Counter.text = "";
            for (int i = 0; i != (int)OptionGroupSingleton<BountyHunterOptions>.Instance.SuccessfulKillsQuota; i++)
            {
                Counter.text = $"{Counter.text}<sprite=6>";
            }

            IsOpen = true;

            myButton = gameObject.AddComponent<PassiveButton>();
            myButton.OnClick.AddListener((Action)(() =>
            {
                ToggleHud(!IsOpen);
            }));
        }
        public void OnNewTargetGenerated(NetworkedPlayerInfo info)
        {
            if (myPlayer) myPlayer.gameObject.DestroyImmediate();
            //set up PoolablePlayer
            myPlayer = HudManager.Instance.IntroPrefab.CreatePlayer(1, 1, info, false);
            myPlayer.transform.SetParent(gameObject.transform);
            myPlayer.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            myPlayer.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            foreach (var rend in myPlayer.gameObject.GetComponentsInChildren<SpriteRenderer>(true))
            {
                rend.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
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