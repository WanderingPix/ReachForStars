using System.Linq;
using AmongUs.GameOptions;
using HarmonyLib;
using ReachForStars.Features;

namespace ReachForStars
{
    [HarmonyPatch(typeof(RoleManager))]
    public class RoleManagerPatches
    {
        [HarmonyPatch(typeof(RoleManager), nameof(RoleManager.SetRole))]
        [HarmonyPostfix]
        public static void Postfix(RoleManager __instance, ref RoleTypes roleType)
        {
            var type = roleType;
            var roleBehaviour = __instance.AllRoles.First(r => r.Role == type);
            RecolorableUsesCounter.Update(roleBehaviour);
        }
    }
}