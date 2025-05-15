using HarmonyLib;
using ReachForStars.Features;

namespace ReachForStars
{
    [HarmonyPatch(typeof(RoleBehaviour))]
    public class OnShipStatusStart
    {
        [HarmonyPatch(typeof(RoleBehaviour), nameof(RoleBehaviour.Initialize))]
        [HarmonyPostfix]
        public static void Postfix(RoleBehaviour __instance)
        {
            RoleNameTag.SetRoleNameTag(__instance);
        }
    }
}
