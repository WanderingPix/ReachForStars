using System;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using MiraAPI.GameOptions;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReachForStars.Roles.Neutrals.BountyHunter;

public class BountyHud : MonoBehaviour
{
    public SpriteRenderer myRend;
    public TextMeshPro Counter;
    public TextMeshPro WantedText;
    public AspectPosition myPos;
    public PoolablePlayer myPlayer;
    public PassiveButton myButton;
    public Animator myAnim;
    public BoxCollider2D myCollider;
    public bool IsOpen;

    public void Start()
    {
        Counter = gameObject.transform.GetChild(2).GetComponent<TextMeshPro>();
        Counter.text = "";
        for (var i = 0; i != (int)OptionGroupSingleton<BountyHunterOptions>.Instance.SuccessfulKillsQuota; i++)
            Counter.text = $"{Counter.text}<sprite=6>";

        WantedText = gameObject.transform.GetChild(1).GetComponent<TextMeshPro>();

        myRend = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

        myPos = gameObject.AddComponent<AspectPosition>();
        myPos.Alignment = AspectPosition.EdgeAlignments.Top;
        myPos.DistanceFromEdge = new Vector3(0f, 1.5f, 0f);
        myPos.AdjustPosition();

        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,
            gameObject.transform.localPosition.y, 0f);

        myAnim = myRend.gameObject.AddComponent<Animator>();

        IsOpen = true;

        myCollider = gameObject.AddComponent<BoxCollider2D>();
        myCollider.size = new Vector2(3f, 3f);
        myCollider.isTrigger = true;

        myButton = myRend.gameObject.AddComponent<PassiveButton>();
        var Event = new Button.ButtonClickedEvent();
        Event.AddListener(OnClick());
        myButton.OnClick = Event;
        myButton.Colliders = new Il2CppReferenceArray<Collider2D>([myCollider]);
        myButton.ClickMask = myCollider;
        gameObject.transform.localPosition =
            new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 20f);
    }

    public void ToggleHud(bool Show)
    {
        if (Show)
            myAnim.runtimeAnimatorController = Assets.BountyHudOpenAnimController.LoadAsset();
        else if (!Show) myAnim.runtimeAnimatorController = Assets.BountyHudCloseAnimController.LoadAsset();
        myPlayer.gameObject.SetActive(Show);
        Counter.gameObject.SetActive(Show);
        WantedText.gameObject.SetActive(Show);
        IsOpen = Show;
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
        foreach (var cosmetic in myPlayer.cosmetics.gameObject.GetComponentsInChildren<SpriteRenderer>())
            cosmetic.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        myPlayer.transform.SetParent(gameObject.transform);
        myPlayer.transform.localPosition = new Vector3(0f, -0.15f, 0f);
        foreach (var rend in myPlayer.gameObject.GetComponentsInChildren<SpriteRenderer>(true))
            rend.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(gameObject.transform, 1.2f, 1f, 0.7f));
    }

    public void UpdateCount(int count)
    {
        Counter.text = "";
        int i;
        for (i = 0; i != count; i++) //Killed Bounties
            Counter.text = $"{Counter.text}<sprite=5>";
        for (var r = 0;
             r != (int)OptionGroupSingleton<BountyHunterOptions>.Instance.SuccessfulKillsQuota - count;
             r++) //remaining bounties
            Counter.text = $"{Counter.text}<sprite=6>";
    }
}