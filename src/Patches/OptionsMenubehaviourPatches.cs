using System;
using HarmonyLib;
using MiraAPI.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ReachForStars;

[HarmonyPatch]
public class OptionsMenubehaviourPatches
{
    [HarmonyPatch(typeof(ExitGameButton), nameof(ExitGameButton.OnClick))]
    [HarmonyPrefix]
    public static bool OnExitPrefix(ExitGameButton __instance)
    {
        var popup =
            Object.Instantiate(HudManager.Instance.LobbyTimerExtensionUI.popup,
                HudManager.Instance.transform);
        popup.gameObject.SetActive(true);
        popup.gameObject.transform.localPosition = new Vector3(0f, 0f, -1000f);

        popup.titleTexxt.gameObject.GetComponent<TextTranslatorTMP>().DestroyImmediate();
        popup.titleTexxt.text = "Are you sure you want to leave?";

        popup.button1Text.gameObject.GetComponent<TextTranslatorTMP>().DestroyImmediate();
        popup.button1Text.text = "Leave";
        popup.button1.inactiveSprites.gameObject.GetComponent<SpriteRenderer>().color =
            Palette.ImpostorRed.DarkenColor();
        popup.button1.OnClick.AddListener(OnExitConfirm());

        popup.button2Text.gameObject.GetComponent<TextTranslatorTMP>().DestroyImmediate();
        popup.button2Text.text = "One more game";
        popup.button2.inactiveSprites.gameObject.GetComponent<SpriteRenderer>().color =
            Palette.AcceptedGreen.DarkenColor();
        return false;
    }

    private static Action OnExitConfirm()
    {
        void Listener()
        {
            if (AmongUsClient.Instance)
            {
                AmongUsClient.Instance.ExitGame(DisconnectReasons.ExitGame);
                return;
            }

            SceneChanger.ChangeScene("MainMenu");
        }

        return Listener;
    }
}