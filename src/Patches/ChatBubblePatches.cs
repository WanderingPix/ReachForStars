using HarmonyLib;
using MiraAPI.LocalSettings;
using ReachForStars.Options;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Patches;

[HarmonyPatch]
public class ChatBubblePatches
{
    [HarmonyPatch(typeof(ChatBubble), nameof(ChatBubble.SetText))]
    [HarmonyPostfix]
    public static void ChatBubble_SetText_Postfix(ChatBubble __instance)
    {
        __instance.Background.color = Color.white;
        __instance.Background.material = new(Shader.Find("Sprites/Default"));
        if (LocalSettingsTabSingleton<ClientSettings>.Instance.EnableColorfulBubbles.Value) __instance.Background.color = __instance.playerInfo.Color;
    }

    [HarmonyPatch(typeof(ChatNotification), nameof(ChatNotification.SetUp))]
    [HarmonyPostfix]
    public static void ChatNotification_SetUp_Postfix(ChatNotification __instance, ref PlayerControl sender)
    {
        if (LocalSettingsTabSingleton<ClientSettings>.Instance.EnableColorfulBubbles.Value) __instance.background.color = sender.Data.Color;
    }
}