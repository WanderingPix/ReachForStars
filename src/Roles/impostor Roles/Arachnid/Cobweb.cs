using Reactor.Utilities.Extensions;
using Reactor.Utilities;
using System.Collections;
using UnityEngine;
using MiraAPI.Modifiers;
using MiraAPI.Events;

namespace ReachForStars.Roles.Impostors.Arachnid;

public class Cobweb : MonoBehaviour
{
    public SpriteRenderer myRend;

    public bool ShouldAffectPlayer(PlayerControl Player)
    {
        return Player.Data.Role is not ArachnidRole && !Player.Data.IsDead && Player.HasModifier<SlowedDownModifier>();
    }
    public void Start()
    {
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        myRend = gameObject.AddComponent<SpriteRenderer>();
        Coroutines.Start(DoSpawnAnimation(myRend));
    }
    public IEnumerator DoSpawnAnimation(SpriteRenderer rend)
    {
        SoundManager.Instance.PlaySound(Assets.CobwebSFX.LoadAsset(), false, 1f);
        rend.sprite = Assets.Cobweb0.LoadAsset();
        yield return new WaitForSeconds(0.125f);

        rend.sprite = Assets.Cobweb1.LoadAsset();
        yield return new WaitForSeconds(0.125f);

        rend.sprite = Assets.Cobweb2.LoadAsset();
        yield return new WaitForSeconds(0.125f);

        yield break;
    }

    public void FixedUpdate()
    {
        foreach (var player in PlayerControl.AllPlayerControls)
        {
            if (ShouldAffectPlayer(player) && Vector2.Distance(player.GetTruePosition(), transform.position) < 1f)
            {
                player.AddModifier<SlowedDownModifier>();
            }
        }
    }
    [RegisterEvent]
    public static void OnMeetingEnd(MiraAPI.Events.Vanilla.Meeting.EndMeetingEvent @event)
    {
        foreach (var cobweb in Object.FindObjectsOfType<Cobweb>())
        {
            cobweb.DestroyImmediate();
        }
    }
}
