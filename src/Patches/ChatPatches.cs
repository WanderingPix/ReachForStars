using HarmonyLib;
using ReachForStars.Features;

namespace ReachForStars;

[HarmonyPatch]
public class ChatPatches
{
    [HarmonyPatch(typeof(ChatBubble), nameof(ChatBubble.SetText))]
    public static void Postfix(ChatBubble __instance, ref string chatText)
    {
        __instance.TextArea.m_spriteAsset = Assets.EmojiIndex.LoadAsset();
        __instance.TextArea.text = Emojis.ReformatForEmojis(chatText);
    }
}