using HarmonyLib;
using UnityEngine;

namespace ReachForStars
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public class OnHudStart
    {
        public static void Postfix(HudManager __instance)
        {
            foreach (SpriteRenderer rend in __instance.GetComponentsInChildren<SpriteRenderer>(true))
            {
                rend.gameObject.transform.localScale *= 1f; //Once chipseq's client settings pr is merged, add a slider for this 
            }
        }
    }
}
