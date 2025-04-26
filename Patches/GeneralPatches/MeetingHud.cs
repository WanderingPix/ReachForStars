using HarmonyLib;
using MiraAPI.GameOptions;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Modifiers;
using ReachForStars.Addons.Flash;
using ReachForStars.Addons.Grandpa;
using ReachForStars.Addons.Child;
using MiraAPI.Hud;
using ReachForStars.Roles.Impostors.Chiller;
using ReachForStars.Roles.Crewmates.Detective;
using ReachForStars.MeetingSettings;

namespace ReachForStars
{
[HarmonyPatch]
    
    public static class HudPatchs
    {
        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.PopulateButtons))]
        public static void Postfix(MeetingHud __instance)
        {
            /*foreach (PlayerVoteArea area in Object.FindObjectsOfType<PlayerVoteArea>())
            {
                if (PlayerControl.LocalPlayer.Data.Role is DetectiveRole det)
                {
                    if (det.Suspects.Contains(area.GetPlayer()))
                    {
                        SpriteRenderer SuspectVisualizer = Object.Instantiate<SpriteRenderer>(area.XMark, area.XMark.transform.parent);
                        SuspectVisualizer.sprite = Assets.PoisonButton.LoadAsset();
                    }
                }
            }*/

            if (OptionGroupSingleton<MeetingOptions>.Instance.NoSkipping)
            {
                __instance.SkippedVoting.gameObject.SetActive(false);
                __instance.SkipVoteButton.gameObject.SetActive(false);
            }
            foreach (PlayerVoteArea area in Object.FindObjectsOfType<PlayerVoteArea>())
            {
                if (PlayerControl.LocalPlayer.Data.Role is DetectiveRole det && det.Suspects.Contains(area.GetPlayer()))
                {
                    SpriteRenderer SuspectVisualizer = Object.Instantiate<SpriteRenderer>(area.XMark, area.XMark.transform.parent);
                    SuspectVisualizer.sprite = Assets.PoisonButton.LoadAsset();
                    SuspectVisualizer.name = "SuspectVisualizer";
                    SuspectVisualizer.gameObject.SetActive(true);
                }
            }
        }
    }
}
