using UnityEngine;

namespace ReachForStars.Features.Minimap;

public static class RefreshedMapBehaviour
{
    public static void SetUp(MapBehaviour __instance)
    {
        __instance.ColorControl.baseColor = PlayerControl.LocalPlayer.Data.Role.TeamColor;
        __instance.StartCoroutine(Effects.Bloop(0f, __instance.transform, SmolUI.ScaleFactor));
        __instance.fadedBackground.transform.localScale = Vector3.one;
    }
}