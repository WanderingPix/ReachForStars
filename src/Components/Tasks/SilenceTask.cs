using System;
using System.Collections;
using System.Collections.Generic;
using Il2CppSystem.Text;
using MiraAPI.GameOptions;
using ReachForStars.Roles.Impostors.Silencer;
using Reactor.Utilities;
using Reactor.Utilities.Attributes;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;

namespace ReachForStars.Components.Tasks;
[RegisterInIl2Cpp(typeof(IHudOverrideTask))]
public class SilenceTask(IntPtr ptr) : SabotageTask(ptr)
{
    public override void AppendTaskText(StringBuilder sb)
    {
        sb.AppendLine("<color=red>Shh! Meetings, reports, and noisemakers are temporarily disabled.</color>");
    }

    public override bool IsComplete => false;

    private GameObject shushTextContainer;
    public Material grayscaleMaterial;
    public float lifetime;
    private float duration = OptionGroupSingleton<SilencerOptions>.Instance.AbilityDuration.Value;

    private float GetCalculatedStrength()
    {
        float fadeInTime = 2;
        float calculatedStrength;
        if (lifetime <= fadeInTime)
        {
            calculatedStrength = Mathf.Lerp(0, 1, lifetime / 0.2f);
        }
        else if (lifetime >= duration)
        {
            calculatedStrength = Mathf.Lerp(1, 0, lifetime / (duration*2f));
        }
        else
        {
            calculatedStrength = 1f;
        }
        return calculatedStrength;
    }
    private void FixedUpdate()
    {
        lifetime += Time.deltaTime;
        grayscaleMaterial.SetFloat("_Strength", GetCalculatedStrength());
        if (lifetime >= duration * 1.1f)
        {
            Remove();
        }
    }

    public void Remove()
    {
        SoundManager.Instance.gameObject.SetActive(true);
        CameraFXComponent.Instance.materials.Remove(grayscaleMaterial);
        grayscaleMaterial.Destroy();
        
        gameObject.Destroy();
    }

    private void Start()
    {
        SoundManager.Instance.gameObject.SetActive(false);
        grayscaleMaterial = new(Assets.GrayscaleMaterial.LoadAsset());
        CameraFXComponent.Instance?.materials.Add(grayscaleMaterial);
        shushTextContainer = new GameObject("shushText");
        shushTextContainer.transform.parent = HudManager.Instance.transform;
        shushTextContainer.transform.localPosition = Vector3.zero;
        
        var letters = new List<TextMeshPro>();
        int xPos = -3;
        foreach (char c in "Shush!")
        {
            var tmp = UnityObject.Instantiate(HudManager.Instance.KillButton.buttonLabelText, shushTextContainer.transform);
            tmp.GetComponent<TextTranslatorTMP>().Destroy();
            tmp.text = c.ToString();
            tmp.transform.localPosition = new Vector3(xPos, 0, -10);
            tmp.transform.localScale = Vector3.zero;
            tmp.fontSize = 64;
            tmp.fontStyle = FontStyles.Italic;
            xPos++;
            tmp.color = Color.gray;
            letters.Add(tmp);
        }

        Coroutines.Start(CoAnimateShushText(letters));
    }

    private IEnumerator CoAnimateShushText(List<TextMeshPro> letters)
    {
        var wait = new WaitForSeconds(0.2f);
        foreach (TextMeshPro tmp in letters)
        {
            Coroutines.Start(CoAnimateShushLetter(tmp, 0.7f));
            yield return wait;
        }
        yield return new WaitForSeconds(1f);
        shushTextContainer.gameObject.Destroy();
        yield break;
    }

    private IEnumerator CoAnimateShushLetter(TextMeshPro tmp, float duration)
    {
        yield return tmp.StartCoroutine(Effects.Bloop(0, tmp.transform, 3, duration*0.3f));
        yield return new WaitForSeconds(duration*0.1f);
        yield return tmp.StartCoroutine(Effects.Slide2D(tmp.transform, tmp.transform.localPosition, new(tmp.transform.localPosition.x, 10), duration*0.6f));
        tmp.gameObject.Destroy();
        yield break;
    }
}