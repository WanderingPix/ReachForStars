using HarmonyLib;
using MiraAPI.Utilities;
using MiraAPI.Roles;
using MiraAPI.GameOptions;
using ReachForStars.GameEndSettings;
using System.Linq;

namespace ReachForStars
{
    [HarmonyPatch(typeof(ChatBubble), nameof(ChatBubble.SetText))]
    public class ChatPatches
    {
        public static void Postfix(ChatBubble __instance, ref string chatText)
        {
            __instance.TextArea.m_defaultSpriteAsset = Assets.EmojiIndex.LoadAsset();
            string FinalText = chatText;

            if (FinalText.ToLower().Contains("shrug")) FinalText = FinalText.Replace("shrug", "<sprite=0>");
            if (FinalText.ToLower().Contains("heart")) FinalText = FinalText.Replace("heart", "<sprite=1>");
            if (FinalText.ToLower().Contains("heh")) FinalText = FinalText.Replace("heh", "<sprite=2>");
            if (FinalText.ToLower().Contains("fire")) FinalText = FinalText.Replace("fire", "<sprite=3>");
            if (FinalText.ToLower().Contains("sob")) FinalText = FinalText.Replace("sob", "<sprite=4>");

            __instance.TextArea.text = FinalText;
        }
    }
}
