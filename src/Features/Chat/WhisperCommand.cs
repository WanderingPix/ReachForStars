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
            if (Target.ToLower().Contains("/msg " + player.Data.PlayerName.ToLower()))
            {
                if (player == PlayerControl.LocalPlayer)
                {
                    return $"{bubble.playerInfo.PlayerName} is whispering to {player.Data.PlayerName}";
                }
                else
                {
                    return 
                }
                bubble.Background.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            }
        }
        return Target;
    }
}
} 
