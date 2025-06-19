using Discord;
using HarmonyLib;
using ReachForStars.Features;

namespace ReachForStars
{
    [HarmonyPatch]
    public class DiscordRPCPatches
    {
        [HarmonyPatch(typeof(ActivityManager), nameof(ActivityManager.UpdateActivity))]
        public static void Postfix([HarmonyArgument(0)] Activity activity)
        {
            activity.Assets.LargeText = "Reach For Stars";
        }
    }
}
