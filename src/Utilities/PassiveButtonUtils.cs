using System;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;
using UnityEngine.UI;

namespace ReachForStars.Utilities;

public static class PassiveButtonUtils
{
    public static PassiveButton CreatePassiveButton(GameObject go, Sprite active, Sprite inactive, Action listener)
    {
        var passive = go.AddComponent<PassiveButton>();

        passive.activeSprites = new GameObject("Active");
        passive.activeSprites.transform.SetParent(go.transform);
        passive.activeSprites.AddComponent<SpriteRenderer>().sprite = active;

        passive.inactiveSprites = new GameObject("Inactive");
        passive.inactiveSprites.transform.SetParent(go.transform);
        passive.inactiveSprites.AddComponent<SpriteRenderer>().sprite = inactive;

        passive.ClickMask = go.AddComponent<BoxCollider2D>();
        passive.Colliders = new Il2CppReferenceArray<Collider2D>([passive.ClickMask]);

        passive.OnClick = new Button.ButtonClickedEvent();
        passive.OnClick.AddListener(listener);

        return passive;
    }
}