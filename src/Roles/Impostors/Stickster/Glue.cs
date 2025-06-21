using System.Collections;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities;
using UnityEngine;
using Random = System.Random;

namespace ReachForStars.Roles.Impostors.Stickster;

public class Glue : MonoBehaviour
{
    public static LoadableResourceAsset[] Variations = new[]
    {
        Assets.GlueVar0,
        Assets.GlueVar1,
        Assets.GlueVar2
    };

    public SpriteRenderer myRend;

    public void Start()
    {
        gameObject.transform.localScale = new Vector3(0.45f, 0.45f, 1f);
        myRend = gameObject.AddComponent<SpriteRenderer>();
        Coroutines.Start(DoSpawnAnimation(myRend));
    }

    public void FixedUpdate()
    {
        foreach (var player in PlayerControl.AllPlayerControls)
            if (ShouldAffectPlayer(player) && Vector2.Distance(player.GetTruePosition(), transform.position) < 1f)
                player.AddModifier<SlowedDownModifier>();
    }

    public bool ShouldAffectPlayer(PlayerControl Player)
    {
        return Player.Data.Role is not SticksterRole && !Player.Data.IsDead &&
               !Player.HasModifier<SlowedDownModifier>();
    }

    public IEnumerator DoSpawnAnimation(SpriteRenderer rend)
    {
        if (Helpers.CheckChance(50)) myRend.flipX = true;
        SoundManager.Instance.PlaySound(Assets.GlueSFX.LoadAsset(), false);
        rend.sprite = Assets.Glue0.LoadAsset();
        yield return new WaitForSeconds(0.1f);

        rend.sprite = Assets.Glue1.LoadAsset();
        yield return new WaitForSeconds(0.1f);

        SoundManager.Instance.PlaySound(Assets.GlueSFX.LoadAsset(), false);
        rend.sprite = Assets.Glue2.LoadAsset();
        yield return new WaitForSeconds(0.1f);

        rend.sprite = getRandomGlueSprite().LoadAsset();
        yield return new WaitForSeconds(0.1f);

        yield break;
    }

    public static LoadableResourceAsset getRandomGlueSprite()
    {
        var rng = new Random();
        int index = rng.Next(0, Variations.Length);
        return Variations[index];
    }
}