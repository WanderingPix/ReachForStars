using System;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ReachForStars;

[HarmonyPatch]
public class OptionsMenuBehaviourPatches
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
        popup.button1.OnClick.AddListener(OnExitPopupButtonClicked(popup, true));

        popup.button2Text.gameObject.GetComponent<TextTranslatorTMP>().DestroyImmediate();
        popup.button2Text.text = "One more game";
        popup.button1.OnClick.AddListener(OnExitPopupButtonClicked(popup, false));
        return false;
    }

    private static Action OnExitPopupButtonClicked(InfoTextBox popup, bool leave)
    {
        void Listener()
        {
            if (AmongUsClient.Instance && leave)
            {
                AmongUsClient.Instance.ExitGame(DisconnectReasons.ExitGame);
                SceneChanger.ChangeScene("MainMenu");
            }

            popup.gameObject.DestroyImmediate();
        }

        return Listener;
    }
}