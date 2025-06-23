using System.Linq;
using HarmonyLib;
using ReachForStars.Roles.Crewmates.Detective;

namespace ReachForStars.Patches;

[HarmonyPatch]
public static class MeetingHudPatches
{
    [HarmonyPatch(typeof(PlayerVoteArea), nameof(PlayerVoteArea.SetCosmetics))]
    [HarmonyPostfix]
    public static void PlayerVoteAreaSetCosmeticsPostfix(PlayerVoteArea __instance, ref NetworkedPlayerInfo playerInfo)
    {
        var targetId = playerInfo.PlayerId;
        if (PlayerControl.LocalPlayer.Data.Role is DetectiveRole det &&
            det.Suspects.Where(x => x.PlayerId == targetId).Count() > 0) det.SetUpVoteArea(__instance);
    }
}