using HarmonyLib;
using MiraAPI.Modifiers;
using ReachForStars.Modifiers;
using ReachForStars.Roles.Impostors.Mole;

namespace ReachForStars.Patches;

[HarmonyPatch]
public class PlayerPhysicsPatches
{
    [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.FixedUpdate))]
    [HarmonyPrefix]
    public static bool FixedUpdatePrefix(PlayerPhysics __instance)
    {
        bool handleTunnelingPhysics = __instance.myPlayer.HasModifier<TunnelingModifier>();
        if (handleTunnelingPhysics)
        {
            __instance.myPlayer.GetModifier<TunnelingModifier>()?.PlayerPhysicsFixedUpdate();
        }
        return !handleTunnelingPhysics;
    }
}