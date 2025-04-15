using System;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;

namespace ReachForStars
{
    [HarmonyPatch]

    public class CreditsPatches
    {
        [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
        [HarmonyPrefix]

        public static void Prefix(MainMenuManager __instance)
        {   
            if (__instance.newsButton != null)
            {

                var CreditsButton = UnityEngine.Object.Instantiate(__instance.newsButton, null);
                CreditsButton.name = "CreditsButton";

                CreditsButton.transform.localScale = new Vector3(0.84f, 0.84f, 1f);

                PassiveButton passive = CreditsButton.GetComponent<PassiveButton>();
                passive.OnClick = new UnityEngine.UI.Button.ButtonClickedEvent();

                passive.inactiveSprites.GetComponent<SpriteRenderer>().color = new Color32(242, 145, 255, 255);
                passive.activeSprites.GetComponent<SpriteRenderer>().color = new Color32(252, 234, 255, 255);

                CreditsButton.gameObject.transform.SetParent(GameObject.Find("RightPanel").transform);
                var pos = CreditsButton.gameObject.AddComponent<AspectPosition>();

                CreditsButton.buttonText.alignment = TextAlignmentOptions.Center;
                
                passive.OnClick.AddListener((Action)(() =>
                {
                }));

                var text = CreditsButton.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>();

                __instance.StartCoroutine(Effects.Lerp(0.1f, new System.Action<float>((p) =>
                {
                    text.text = $"Credits";
                    pos.AdjustPosition();
                })));

                CreditsButton.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                CreditsButton.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                CreditsButton.GetComponent<NewsCountButton>().DestroyImmediate();
                CreditsButton.transform.GetChild(3).gameObject.DestroyImmediate();
            }
        }
    }
}