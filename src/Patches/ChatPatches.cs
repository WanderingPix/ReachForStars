using HarmonyLib;
using MiraAPI.Modifiers;
using ReachForStars.Roles.Impostors.Blackmailer;
using Reactor.Utilities;

namespace ReachForStars;

[HarmonyPatch]
public class ChatPatches
{
    [HarmonyPatch(typeof(ChatController), nameof(ChatController.OnEnable))]
    [HarmonyPostfix]
    public static void OnChatOpenPostfix(ChatController __instance)
    {
        BlackmailedModifier? bmed = PlayerControl.LocalPlayer.GetModifier<BlackmailedModifier>();
        if (bmed != null && bmed.HasOpenedChatOnce == false) Coroutines.Start(bmed.CoAnimate(__instance));
    }
}