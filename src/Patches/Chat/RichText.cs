using System;
using System.Collections.Generic;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;
using System.Linq;

namespace ReachForStars
{
    public static class RichText
    {
        /// <summary>
        /// Adds basic rich text elements to strings, uses discord rich text syntax, doesnt work on non TextMeshPro objects.
        /// </summary>
        /// <param name="Target">The string to apply the rich text to</param>

        /// Called by Patches/Chat/ChatPatch
        public static string RichtextIfy(this string Target)
        {
            ///Rich Text behavior
            if (Target.Contains("***"))
            {
                Target = Target.Replace("***", "<b><i>");
            }
            else if (Target.Contains("**"))
            {
                Target = Target.Replace("**", "<b>");
            }
            else if (Target.Contains("*"))
            {
                Target = Target.Replace("*", "<i>");
            }
            else if (Target.Contains("__"))
            {
                Target = Target.Replace("__", "<u>");
            }
            else if (Target.Contains("--"))
            {
                Target = Target.Replace("--", "<s>");
            }
            return Target;
        }
    }
}