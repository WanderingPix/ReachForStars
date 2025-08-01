using HarmonyLib;
using MiraAPI.Modifiers;
using ReachForStars.Modifiers;
using ReachForStars.Roles.Neutrals.GhostBuster;

namespace ReachForStars.Patches;

[HarmonyPatch]
public class PlayerControlPatches
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    [HarmonyPostfix]
    public static void Postfix(PlayerControl __instance)
    {
        if ((PlayerControl.LocalPlayer.HasModifier<CanSeeGhostsModifier>() &&
             !__instance.HasModifier<VacuumedModifier>() && __instance.Data.IsDead) ||
            __instance == PlayerControl.LocalPlayer || PlayerControl.LocalPlayer.Data.IsDead) __instance.Visible = true;
        else __instance.Visible = !__instance.Data.IsDead;
    }
}