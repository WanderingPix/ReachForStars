using System;
using HarmonyLib;
using ReachForStars.Features;

namespace ReachForStars
{
    [HarmonyPatch]
    public class ChatPatches
    {
        [HarmonyPatch(typeof(TextBoxTMP), nameof(TextBoxTMP.Start))]
        public static void Postfix(TextBoxTMP __instance)
        {
            __instance.AllowPaste = true;
            __instance.AllowSymbols = true;
            __instance.allowAllCharacters = true;
            __instance.characterLimit = int.MaxValue;

            __instance.outputText.m_spriteAsset = Assets.EmojiIndex.LoadAsset();
            __instance.OnChange.AddListener((Action)(() =>
            {
                __instance.SetText(Emojis.ReformatForEmojis(__instance.text));            
            }));
        }
    }
}
