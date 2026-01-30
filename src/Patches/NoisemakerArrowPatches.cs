using HarmonyLib;
using ReachForStars.Components.Tasks;
using Reactor.Utilities.Extensions;

namespace ReachForStars.Patches;

[HarmonyPatch]
public class NoisemakerArrowPatches
{
    [HarmonyPatch(typeof(NoisemakerArrow), nameof(NoisemakerArrow.UpdatePosition))]
    [HarmonyPostfix]
    public static void NoisemakerArrow_UpdatePosition_Postfix(NoisemakerArrow __instance)
    {
        if (PlayerTask.PlayerHasTaskOfType<SilenceTask>(PlayerControl.LocalPlayer))
        {
            __instance.gameObject.Destroy();
        }
    }
}