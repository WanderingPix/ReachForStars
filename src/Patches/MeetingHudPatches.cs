using System.Linq;
using HarmonyLib;
using ReachForStars.Roles.Crewmates.Detective;
using ReachForStars.Roles.Crewmates.Executionner;

namespace ReachForStars.Patches;

[HarmonyPatch]
public static class MeetingHudPatches
{
    [HarmonyPatch(typeof(PlayerVoteArea), nameof(PlayerVoteArea.SetCosmetics))]
    [HarmonyPostfix]
    public static void PlayerVoteAreaSetCosmeticsPostfix(PlayerVoteArea __instance, ref NetworkedPlayerInfo playerInfo)
    {
        var id = playerInfo.PlayerId;
        if (PlayerControl.LocalPlayer.Data.Role is DetectiveRole det &&
            det.Suspects.Where(x => x.PlayerId == id).Count() > 0) det.SetUpVoteArea(__instance);

        if (PlayerControl.LocalPlayer.Data.Role is ExecutionerRole exe &&
            !PlayerControl.LocalPlayer.Data.IsDead /* && PlayerControl.LocalPlayer.GetTasksLeft() == 0*/)
            exe.SetUpVoteArea(__instance);
    }
}