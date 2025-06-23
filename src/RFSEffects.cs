using System.Collections;
using UnityEngine;

namespace ReachForStars;

public static class RFSEffects
{
    public static IEnumerator SwayY(this Transform target, float duration, float HalfHeight)
    {
        var origin = target.localPosition;
        for (var timer = 0f; timer < duration; timer += Time.deltaTime)
        {
            var num = timer / duration;
            target.localPosition = origin + Vector3.up * (HalfHeight * Mathf.Sin(num * 30f) * (1f - num));
            yield return null;
        }

        target.transform.localPosition = origin;
        origin = default;
        yield break;
    }
}