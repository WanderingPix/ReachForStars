using HarmonyLib;
using ReachForStars.Features;

namespace ReachForStars;

[HarmonyPatch]
public class OptionsMenuBehaviourPatches
{
    [HarmonyPatch(typeof(ExitGameButton), nameof(ExitGameButton.OnClick))]
    [HarmonyPrefix]
    public static bool OnExitPrefix(ExitGameButton __instance)
    {
        ConfirmExitDialog.ShowDialog();
        return false;
    }
}