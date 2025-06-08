using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using System.Collections.Generic;
using UnityEngine.UI;
using ReachForStars.Utilities;
using Reactor.Utilities;
using IEnumerator = System.Collections.IEnumerator;
using Reactor.Utilities.Extensions;

namespace ReachForStars.Roles.Impostors.Shadow;

public class ShadowEffect : MonoBehaviour
{
    public void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new(1f, 1f, 1f, 0.1f);
        Coroutines.Start(DoEclipse(CustomButtonSingleton<Eclipse>.Instance.EffectDuration));

        HudManager.Instance.ShadowQuad.enabled = false;
    }
    public IEnumerator DoEclipse(float Duration)
    {
        yield return HudManager.Instance.StartCoroutine(Effects.ScaleIn(gameObject.transform, 0f, 1000f, 5f));
        yield return new WaitForSeconds(Duration);
        yield return HudManager.Instance.StartCoroutine(Effects.ScaleIn(gameObject.transform, 1000f, 0f, 5f));
        HudManager.Instance.ShadowQuad.enabled = true;
        gameObject.DestroyImmediate();
        yield break;
    }
}