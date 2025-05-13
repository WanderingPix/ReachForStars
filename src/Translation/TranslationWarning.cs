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
            ControllerManager.Instance.OpenOverlayMenu("TranslationWarning", null);
            var Warning = Object.Instantiate<ControllerUIElement>(null);
            Warning._label.text = "Translation Warning\n For better translations, please restart your game.";
            Warning._label.alignment = TextAnchor.MiddleCenter;      
            Warning._highlightAmount = 5f;
        }
    }
}