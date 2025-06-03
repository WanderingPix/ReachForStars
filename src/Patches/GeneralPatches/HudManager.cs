using HarmonyLib;
using ReachForStars.Features;
using UnityEngine;

namespace ReachForStars
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public class OnHudStart
    {
        public static void Postfix(HudManager __instance)
        {
            SmolUI.ResizeUI();
        }
    }
}
