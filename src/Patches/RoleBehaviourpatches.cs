using HarmonyLib;
using ReachForStars.Features;

namespace ReachForStars
{
    [HarmonyPatch(typeof(RoleBehaviour))]
    public class RoleBehaviourPatches
    {
        [HarmonyPatch(typeof(RoleBehaviour), nameof(RoleBehaviour.Initialize))]
        [HarmonyPostfix]
        public static void Postfix(RoleBehaviour __instance)
        {
            RecolorableUsesCounter.Update(__instance);
        }
    }
}