using System;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using Object = UnityEngine.Object;
using UnityEngine;
using Rewired.Demos.GamepadTemplateUI;

namespace ReachForStars
{
    [HarmonyPatch]

    public class TransWarning
    {
        [HarmonyPatch(typeof(TranslationController), nameof(TranslationController.SetLanguage))]
        [HarmonyPrefix]

        public static void Postfix(TranslationController __instance)
        {
            PopupDialog Warning = Object.Instantiate<PopupDialog>(Object.FindObjectOfType<PopupDialog>());
            Warning.currentProgressText = "Translation Warning\n For better translations, please restart your game.";
        }
    }
}