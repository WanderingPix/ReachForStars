using Discord;
using HarmonyLib;

namespace ReachForStars;

[HarmonyPatch]
public class DiscordRPCPatches
{
    [HarmonyPatch(typeof(ActivityManager), nameof(ActivityManager.UpdateActivity))]
    public static void Postfix([HarmonyArgument(0)] Activity activity)
    {
        activity.Details = "Reach For The Stars";
    }
}