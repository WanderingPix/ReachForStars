using System.Collections;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Components;

public class BlackmailEmblem : MonoBehaviour
{
    public GameObject SpinnyThing;
    public SpriteRenderer Gun;
    public SpriteRenderer ImpostorRenderer;
    public GameObject Spark;
    public PlayerControl Bmer;

    public void Start()
    {
        SpinnyThing = gameObject.transform.GetChild(0).gameObject;
        Gun = gameObject.transform.GetChild(1).GetChild(3).GetComponent<SpriteRenderer>();
        ImpostorRenderer = gameObject.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>();
        Spark = gameObject.transform.GetChild(1).GetChild(2).gameObject;
        Bmer = PlayerControl.LocalPlayer; //TODO: REMOVE PLACEHOLDER!!!
        Coroutines.Start(CoAnimate());
    }

    public IEnumerator CoAnimate()
    {
        Spark.SetActive(false);
        Gun.gameObject.SetActive(false);
        ImpostorRenderer.material = new Material(Shader.Find("Unlit/PlayerShader"));
        PlayerMaterial.SetColors(Bmer.cosmetics.ColorId, ImpostorRenderer);
        HudManager.Instance.StartCoroutine(Effects.Rotate2D(SpinnyThing.transform, 0f, 1800, 10f));

        yield return new WaitForSeconds(2f);

        Spark.SetActive(true);
        HudManager.Instance.StartCoroutine(Effects.Rotate2D(Spark.transform, 0f, 45, 5f));
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(Spark.transform, 0f, .7f, 0.4f));
        Gun.gameObject.SetActive(true);
        Gun.material = new Material(Shader.Find("Unlit/PlayerShader"));
        PlayerMaterial.SetColors(Bmer.cosmetics.ColorId, Gun);
        yield return new WaitForSeconds(2f);
        gameObject.Destroy();
        yield break;
    }
}