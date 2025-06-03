using HarmonyLib;
using ReachForStars.Features;
using ReachForStars.Roles.Neutrals.CursedSoul;

namespace ReachForStars
{
    [HarmonyPatch(typeof(RoleBehaviour))]
    public class RoleBehaviourPatches
    {
        [HarmonyPatch(typeof(RoleBehaviour), nameof(RoleBehaviour.Initialize))]
        [HarmonyPostfix]
        public static void Postfix(RoleBehaviour __instance)
        {
            RoleNameTag.SetRoleNameTag(__instance);
        }
    }
}
