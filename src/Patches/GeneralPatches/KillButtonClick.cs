using HarmonyLib;
using ReachForStars.Roles.Neutrals.Roles;

namespace ReachForStars
{
[HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    
    public static class OnKillButtonClick
    {
        public static void Prefix(KillButton __instance)
        {
            if (PlayerControl.LocalPlayer.Data.Role is BountyHunterRole BH && __instance.Target == BH.BountyTarget)
            {
                BH.OnTargetKill();
            }
        }
    }
}