using System.Collections;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

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

    public static void SpawnTextOverlay(this HudManager hudman, string Text)
    {
        var textOverlay = Object.Instantiate(hudman.TaskCompleteOverlay, hudman.TaskCompleteOverlay.parent)
            .GetComponent<TextMeshPro>();

        textOverlay.GetComponent<TextTranslatorTMP>().DestroyImmediate();
        textOverlay.text = Text;

        Coroutines.Start(AnimateObjectOnScreen(textOverlay.gameObject));
    }

    private static IEnumerator AnimateObjectOnScreen(GameObject go)
    {
        if (Constants.ShouldPlaySfx()) SoundManager.Instance.PlaySound(HudManager.Instance.TaskCompleteSound, false);
        go.SetActive(true);
        yield return Effects.Slide2D(go.transform, new Vector2(0f, -8f), Vector2.zero, 0.25f);
        for (var time = 0f; time < 0.75f; time += Time.deltaTime) yield return null;
        yield return Effects.Slide2D(go.transform, Vector2.zero, new Vector2(0f, 8f), 0.25f);
        go.DestroyImmediate();
        yield break;
    }

    public static void ShowKillOverlay(this HudManager hudman, NetworkedPlayerInfo Killer,
        NetworkedPlayerInfo ToBeKilled, Color color)
    {
        hudman.KillOverlay.ShowKillAnimation(Killer, ToBeKilled);
        hudman.KillOverlay.flameParent.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
    }

    public static IEnumerator CoShowKillOverlay(this HudManager hudman, NetworkedPlayerInfo Killer,
        NetworkedPlayerInfo ToBeKilled, Color color)
    {
        hudman.KillOverlay.ShowKillAnimation(Killer, ToBeKilled);

        SpriteRenderer Flame = hudman.KillOverlay.flameParent.transform.GetChild(0).GetComponent<SpriteRenderer>();
        Flame.color = color;
        yield return new WaitForSeconds(3f);
        Flame.color = Color.white;
    }

    public static void FlashScreen(this HudManager hudman, Color color, AudioClip sound = null)
    {
        Coroutines.Start(CoFlashScreen(hudman, color, sound));
    }

    public static IEnumerator CoFlashScreen(this HudManager hudman, Color color, AudioClip sound)
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        bool light = false;
        hudman.FullScreen.color = new(color.r, color.g, color.b, 0.372549027f);
        hudman.FullScreen.gameObject.SetActive(!hudman.FullScreen.gameObject.activeSelf);
        SoundManager.Instance.PlaySound(sound, false, 1f, null);
        if (hudman.lightFlashHandle == null)
        {
            hudman.lightFlashHandle = DestroyableSingleton<DualshockLightManager>.Instance.AllocateLight();
            hudman.lightFlashHandle.color = color;
            hudman.lightFlashHandle.intensity = 1f;
        }

        light = !light;
        yield return wait;
        hudman.FullScreen.gameObject.SetActive(false);
        yield break;
    }
}