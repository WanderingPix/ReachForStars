using HarmonyLib;
using MiraAPI.GameOptions;
using MiraAPI.Modifiers;
using ReachForStars.Roles.Impostors.Stickster;
using UnityEngine;

namespace ReachForStars;

[HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.FixedUpdate))]
public class PlayerPhysicsPatches
{
    [HarmonyPostfix]
    public static void Postfix(PlayerPhysics __instance)
    {
        if (__instance.myPlayer.HasModifier<SlowedDownModifier>())
            __instance.body.velocity *=
                new Vector2(OptionGroupSingleton<SticksterOptions>.Instance.SlowedDownSpeed.Value,
                    OptionGroupSingleton<SticksterOptions>.Instance.SlowedDownSpeed.Value / 2f);
    }
}