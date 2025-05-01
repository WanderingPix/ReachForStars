using System;
using System.Collections.Generic;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;
using System.Linq;

namespace ReachForStars
{
public static class Whispering
{
    /// <summary>
    /// Does the whispering logic
    /// </summary>
    /// <param name="Target"> The chatbubble's text </param>
    /// <param name="bubble"> The chatbubble instance </param>

    /// Called by Patches/Chat/ChatPatch
    public static string TryWhisper(this string Target, ChatBubble bubble)
    {
        foreach (var player in PlayerControl.AllPlayerControls)
        {
            if (Target.Contains("/msg " + player.Data.PlayerName))
            {
                if (PlayerControl.LocalPlayer.Data.PlayerName == bubble.playerInfo.PlayerName)
                {
                    Target = Target.Replace("/msg " + player.Data.PlayerName, bubble.playerInfo.PlayerName + " Whispers to you: ");
                }
                if (bubble.playerInfo.PlayerName == PlayerControl.LocalPlayer.Data.PlayerName)
                {
                    Target = Target.Replace("/msg " + player.Data.PlayerName, "You whisper to " + player.Data.PlayerName + " :");
                }
                else
                {
                    Target = bubble.playerInfo.PlayerName + " is whispering to " + player.Data.PlayerName;
                }
                bubble.Background.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            }
        }
        return Target;
    }
}
} 
