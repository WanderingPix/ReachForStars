using UnityEngine;
using TMPro;
using System;
using Object = UnityEngine.Object;
using Reactor.Utilities;
using System.Collections;

namespace ReachForStars.Utilities;

public static class HudManUtils
{
    public static GameObject SpawnHnSPopUp(this HudManager Hudman, PlayerControl source, string Sentence)
    {
        var popup = GameManagerCreator.Instance.HideAndSeekManagerPrefab.DeathPopupPrefab;
        Object.Instantiate(popup, HudManager.Instance.transform.parent).Show(source, 5);

        var textobj = popup.transform.GetChild(0);
        textobj.GetComponent<TextMeshPro>().text = Sentence;

        Object.DestroyImmediate(textobj.GetComponent<TextTranslatorTMP>()); 
        
        return popup.gameObject;
    }
}