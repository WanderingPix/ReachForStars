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
        public static string TryWhisper(this string Original, ChatBubble bubble)
        {
            foreach (var player in PlayerControl.AllPlayerControls)
            {
                if (Original.ToLower().StartsWith($"/msg {player.Data.PlayerName.ToLower()}"))
                {
                    if (player == PlayerControl.LocalPlayer)
                    {
                        return $"{bubble.playerInfo.PlayerName} is whispering to you: {Original}";
                    }
                    else
                    {
                        return $"{bubble.playerInfo.PlayerName} is whispering {player.Data.PlayerName}";
                    }
                    bubble.Background.color = new Color(0.65f, 0.65f, 0.65f, 1f);
                }
                else return Original;
            }
        }
    }
}
