using UnityEngine;

namespace ReachForStars.Components;

public class ReviveArrow : ArrowBehaviour
{
    public PlayerControl RevivingPlayer;
    private Transform pivot;
    public float MaxScale => 0.7f;

    private void Start()
    {
        pivot = gameObject.transform.GetChild(0);
        image = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public override void UpdatePosition()
    {
        var main = Camera.main;
        Vector2 vector = target - main.transform.position;
        var num = vector.magnitude / (main.orthographicSize * perc);
        if (image != null) image.enabled = num > minDistanceToShowArrow;

        Vector2 vector2 = main.WorldToViewportPoint(target);
        if (Between(vector2.x, 0f, 1f) && Between(vector2.y, 0f, 1f))
            CloseBehaviour(vector, num);
        else
            DistancedBehaviour(vector2, vector, num, main);

        pivot.transform.LookAt2d(RevivingPlayer.transform.position);
    }

    public override void DistancedBehaviour(Vector2 vpPoint, Vector2 del, float delLen, Camera cam)
    {
        var vector = new Vector2(Mathf.Clamp(vpPoint.x * 2f - 1f, -1f, 1f),
            Mathf.Clamp(vpPoint.y * 2f - 1f, -1f, 1f));
        var safeOrthographicSize = CameraSafeArea.GetSafeOrthographicSize(cam);
        var num = safeOrthographicSize * cam.aspect;
        var vector2 = new Vector3(Mathf.LerpUnclamped(0f, num * 0.8f, vector.x),
            Mathf.LerpUnclamped(0f, safeOrthographicSize * 0.7f, vector.y), 0f);
        transform.position = cam.transform.position + vector2;
        transform.localScale = new Vector3(MaxScale, MaxScale, MaxScale);
    }
}