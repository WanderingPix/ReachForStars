using System;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using Object = UnityEngine.Object;
using UnityEngine;

namespace ReachForStars
{
    [HarmonyPatch]

    public class TransWarning
    {
        [HarmonyPatch(typeof(TranslationController), nameof(TranslationController.SetLanguage))]
        [HarmonyPrefix]

        public static void Postfix(TranslationController __instance)
        {   
            GenericPopup dialogPref = Object.FindObjectOfType<GenericPopup>();
            var Warning = Object.Instantiate<GenericPopup>(dialogPref);
            Warning.TextAreaTMP.text = "Translation Warning\n For better translations, please restart your game.";
            Warning.TextAreaTMP.enableAutoSizing = true;
            Warning.TextAreaTMP.alignment = TMPro.TextAlignmentOptions.Center;
        }
    }
}