using System.Linq;
using HarmonyLib;
using MiraAPI.Modifiers;
using ReachForStars.Components;
using ReachForStars.Modifiers;
using ReachForStars.Roles.Crewmates.Lifesaver;
using Reactor.Utilities.Extensions;

namespace ReachForStars.Patches;

[HarmonyPatch]
public class PlayerControlPatches
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    [HarmonyPostfix]
    public static void PlayerCOntrol_FixedUpdate_Postfix(PlayerControl __instance)
    {
        if (__instance.HasModifier<InvisibleModifier>()) __instance.Visible = false;
        
        if (PlayerControl.LocalPlayer.HasModifier<CanSeeGhostsModifier>() && __instance.Data.IsDead) __instance.Visible = true;
    }
    
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Revive))]
    [HarmonyPostfix]
    public static void PlayerCOntrol_Revive_Postfix(PlayerControl __instance)
    {
        foreach (var body in UnityObject.FindObjectsOfType<DeadBody>().Where(x => x.ParentId == __instance.PlayerId))
        {
            body.gameObject.Destroy();
            __instance.StartCoroutine(__instance.CoSetRole(__instance.Data.RoleWhenAlive.Value, true));
            __instance.AddModifier<RevivedModifier>();
        }
    }
}