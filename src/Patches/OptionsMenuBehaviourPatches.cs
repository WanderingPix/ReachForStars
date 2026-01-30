using HarmonyLib;
using MiraAPI.LocalSettings;
using ReachForStars.Features;
using ReachForStars.Options;

namespace ReachForStars;

[HarmonyPatch]
public class OptionsMenuBehaviourPatches
{
    [HarmonyPatch(typeof(ExitGameButton), nameof(ExitGameButton.OnClick))]
    [HarmonyPrefix]
    public static bool OnExitPrefix(ExitGameButton __instance)
    {
        if (LocalSettingsTabSingleton<ClientSettings>.Instance.ShowLeaveConfirmationPopup.Value)
        {
            ConfirmExitDialog.ShowDialog();
            return false;
        }
        else return true;
    }
}