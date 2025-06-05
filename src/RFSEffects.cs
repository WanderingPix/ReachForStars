using System.Collections;
using MiraAPI.Colors;
using UnityEngine;

namespace ReachForStars;
public static class RFSEffects
{
    public static IEnumerator SwayY(this Transform target, float duration, float HalfHeight)
    {
        Vector3 origin = target.localPosition;
        for (float timer = 0f; timer < duration; timer += Time.deltaTime)
        {
            float num = timer / duration;
            target.localPosition = origin + Vector3.up * (HalfHeight * Mathf.Sin(num * 30f) * (1f - num));
            yield return null;
        }
        target.transform.localPosition = origin;
        origin = default(Vector3);
        yield break;
    }
}