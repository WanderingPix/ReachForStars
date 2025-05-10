using HarmonyLib;
using MiraAPI.GameOptions;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Modifiers;
using ReachForStars.Addons.Flash;
using ReachForStars.Addons.Grandpa;
using ReachForStars.Addons.Child;
using MiraAPI.Hud;
using ReachForStars.Roles.Impostors.Chiller;

namespace ReachForStars
{
[HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.FixedUpdate))]
    
    public static class SpeedControl
    {
        public static void Postfix(PlayerPhysics __instance)
        {
            float Value = 1f;
            if (__instance.AmOwner && GameData.Instance && __instance.myPlayer.CanMove)
            {
                if (PlayerControl.LocalPlayer.HasModifier<ChildModifier>() && !PlayerControl.LocalPlayer.Data.IsDead)
                {
                    Value += 0.75f;
                    Value -= 1f;
                }

                if (PlayerControl.LocalPlayer.HasModifier<FlashModifier>() && !PlayerControl.LocalPlayer.Data.IsDead)
                {
                    Value += PlayerControl.LocalPlayer.GetModifier<FlashModifier>().SpeedMultiplier;
                    Value -= 1f;
                }

                if (PlayerControl.LocalPlayer.HasModifier<WujModifier>() && !PlayerControl.LocalPlayer.Data.IsDead)
                {
                    Value -= 0.5f;
                }
                {
                    Value += 1.5f;
                    Value -= 1f;
                }
            }

            if (Value != 1f)
            {
                __instance.body.velocity *= Value;
            }
        }
    }
}
