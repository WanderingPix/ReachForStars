using System;
using System.Collections;
using System.Linq;
using MiraAPI.Utilities;
using Reactor.Utilities;
using Reactor.Utilities.Attributes;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Components;
[RegisterInIl2Cpp]
public class ThrowableObject : MonoBehaviour
{
    public Vector3 Target;
    private Vector3 _originalpos;
    private float _originalScale;

    public void Initialize(Vector3 target)
    {
        Target = target;
        _originalpos = transform.position;
        _originalScale = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        Coroutines.Start(BeginMovement());
    }

    private IEnumerator BeginMovement()
    {
        float duration = Vector2.Distance(transform.position, Target) / 2;
        Vector3 temp = transform.position;
        for (float time = 0f; time < duration; time += Time.deltaTime)
        {
            float num = time / duration;
            temp.x = Mathf.SmoothStep(_originalpos.x, Target.x, num);
            temp.y = Mathf.Sin(Mathf.PI * time) * 1;
            temp.y = Mathf.Lerp(_originalpos.y, Target.y, time) + temp.y;
            transform.localPosition = temp;
            
            yield return null;
        }
        transform.position = Target;
        Destroy(this);
        yield break;
    }
}