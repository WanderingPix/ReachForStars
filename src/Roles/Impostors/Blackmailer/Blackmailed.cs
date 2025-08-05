using System.Collections;
using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Meeting;
using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Blackmailer;

public class BlackmailedModifier(PlayerControl bmer) : GameModifier
{
    private Sprite _NormalChatBackgroundSprite;

    private Sprite _NormalFreeChatSprite;
    private Sprite _NormalKeyboardBtnSprite;
    private Sprite _NormalQuickChatSprite;
    public bool HasOpenedChatOnce = false;
    public override string ModifierName => "Silenced";

    public override int GetAssignmentChance()
    {
        return 0;
    }

    public override int GetAmountPerGame()
    {
        return 0;
    }

    public override void OnDeactivate()
    {
        ChatController __instance = HudManager.Instance.Chat;
        if (PlayerControl.LocalPlayer == Player)
        {
            __instance.backgroundImage.sprite = _NormalChatBackgroundSprite;

            __instance.freeChatField.textArea.enabled = true;
            __instance.freeChatField.background.sprite = _NormalFreeChatSprite;

            __instance.quickChatButton.enabled = true;
            __instance.quickChatButton.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                _NormalQuickChatSprite;

            __instance.openKeyboardButton.GetComponent<PassiveButton>().enabled = true;
            __instance.openKeyboardButton.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                _NormalKeyboardBtnSprite;
        }
    }

    public IEnumerator CoAnimate(ChatController __instance)
    {
        HasOpenedChatOnce = true;
        //__instance.StartCoroutine(Effects.Shake(__instance.chatScreen.transform, 2f, 0.1f, false, true));
        yield return new WaitForSeconds(1.5f);

        __instance.freeChatField.textArea.enabled = false;
        _NormalFreeChatSprite = __instance.freeChatField.background.sprite;
        Animator anim1 = __instance.freeChatField.background.gameObject.AddComponent<Animator>();
        anim1.runtimeAnimatorController = Assets.FreeChatBreakAnimation.LoadAsset();

        __instance.quickChatButton.enabled = false;
        _NormalQuickChatSprite = __instance.quickChatButton.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        Animator anim2 = __instance.quickChatButton.transform.GetChild(0).gameObject.AddComponent<Animator>();
        anim2.runtimeAnimatorController = Assets.QuickChatBreakAnimation.LoadAsset();

        __instance.openKeyboardButton.GetComponent<PassiveButton>().enabled = false;
        _NormalKeyboardBtnSprite =
            __instance.openKeyboardButton.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        Animator anim3 = __instance.openKeyboardButton.transform.GetChild(0).gameObject.AddComponent<Animator>();
        anim3.runtimeAnimatorController = Assets.KeyboardBtnBreakAnimation.LoadAsset();

        _NormalChatBackgroundSprite = __instance.backgroundImage.sprite;
        Animator anim4 = __instance.backgroundImage.gameObject.AddComponent<Animator>();
        anim4.runtimeAnimatorController = Assets.ChatBackgroundBreakAnimation.LoadAsset();
        yield return new WaitForSeconds(1f);

        anim1.DestroyImmediate();
        anim2.DestroyImmediate();
        anim3.DestroyImmediate();
        anim4.DestroyImmediate();
        yield break;
    }

    [RegisterEvent]
    public static void OnMeetingOverEvent(EndMeetingEvent e)
    {
        foreach (var p in Helpers.GetAlivePlayers())
        {
            if (p.HasModifier<BlackmailedModifier>()) p.RemoveModifier<BlackmailedModifier>();
        }
    }
}