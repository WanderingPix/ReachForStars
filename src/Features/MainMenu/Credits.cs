using System;
using ReachForStars.Utilities;
using UnityEngine;

namespace ReachForStars.Features.MainMenu;

public static class Credits
{
    public static void CreateButton(MainMenuManager __instance)
    {
        GameObject go = new("CreditsButton");
        go.transform.SetParent(__instance.transform.FindChild("LeftPanel"));

        var pos = go.AddComponent<AspectPosition>();
        pos.Alignment = AspectPosition.EdgeAlignments.LeftBottom;

        PassiveButtonUtils.CreatePassiveButton(go, Assets.PListActive.LoadAsset(), Assets.PListInactive.LoadAsset(),
            btnClickListener());
    }

    private static Action btnClickListener()
    {
        void OnClick()
        {
            var go = UnityObject.Instantiate(Assets.CreditsMenuPrefab.LoadAsset());
        }

        return OnClick;
    }
}