using System;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Features;

public static class CollapsibleInfoPane
{
    private static bool _isOpen = true;

    public static void CreateButton(LobbyInfoPane __instance)
    {
        __instance.transform.localScale = new(0.515f, 0.515f, 0.515f);
        AspectPosition pos = __instance.GetComponent<AspectPosition>();
        pos.DistanceFromEdge = new(0.15f, 0.86f, -10f);
        pos.DestroyImmediate();
        PassiveButton collapse =
            UnityObject.Instantiate(__instance.LobbyViewSettingsPane.backButton, __instance.transform);
        collapse.transform.localScale = new(3, 3, 3);
        collapse.transform.localPosition = new(-4.5f, -3.25f, 10f);
        collapse.OnClick = new();
        collapse.OnClick.AddListener(_Listener(__instance));
    }

    private static Action _Listener(LobbyInfoPane __instance)
    {
        void OnClick()
        {
            Coroutines.Start(CoAnimateSlide(__instance));
        }

        return OnClick;
    }

    private static void CoAnimateSlide(LobbyInfoPane __instance)
    {
        if (_isOpen)
        {
            Vector3 dest = new(__instance.transform.localPosition.x - 3f, __instance.transform.localPosition.y);
            Effects.Slide2D(__instance.transform,
                __instance.transform.localPosition,
                dest, 0.4f);
        }
        else if (!_isOpen)
        {
            Vector3 dest = new(__instance.transform.localPosition.x + 3f, __instance.transform.localPosition.y);
            Effects.Slide2D(__instance.transform,
                __instance.transform.localPosition,
                dest, 0.4f);
        }

        _isOpen = !_isOpen;
    }
}