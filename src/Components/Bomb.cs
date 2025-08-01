using System.Collections;
using MiraAPI.Networking;
using Reactor.Utilities;
using UnityEngine;

namespace ReachForStars.Components;

public class Bomb : MonoBehaviour
{
    public PlayerControl Player; //Used for kill cred
    private SpriteRenderer _AreaRenderer;
    private EdgeCollider2D _collider;
    private GameObject _Missile;

    public void Start()
    {
        _AreaRenderer = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        _Missile = transform.GetChild(2).gameObject;
        _collider = GetComponent<EdgeCollider2D>();

        HudManager.Instance.StartCoroutine(Effects.Rotate2D(_AreaRenderer.transform, 0, 1800, 10f));
        Coroutines.Start(CoBeginFall());
    }

    public IEnumerator CoBeginFall()
    {
        HudManager.Instance.StartCoroutine(Effects.Slide2D(_Missile.transform, _Missile.transform.localPosition,
            Vector2.zero, 5f));
        yield return new WaitForSeconds(5f);
        _Missile.SetActive(false);

        foreach (var p in PlayerControl.AllPlayerControls)
            if (p.Collider.IsTouching(_collider))
                Player.CustomMurder(p, MurderResultFlags.DecisionByHost, false, teleportMurderer: false,
                    showKillAnim: false, playKillSound: false);

        yield break;
    }
}