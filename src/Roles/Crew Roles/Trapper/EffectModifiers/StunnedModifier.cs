using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using UnityEngine;
using MiraAPI.Networking;
using MiraAPI.GameOptions;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using MiraAPI.Utilities.Assets;

namespace ReachForStars.Roles.Crewmates.Trapper;

public class StunnedModifier : TimedModifier
{
    public override string ModifierName => "Stunned";

    public override float Duration => 3f;

    PlayerControl Trapper;

    public override void OnMeetingStart()
    {
        Player.MyPhysics.enabled = true;
        Player.RemoveModifier<StunnedModifier>();
    }
    NoisemakerArrow arrow;

    public override void OnActivate()
    {
        //TODO: Stunned anim

        if (PlayerControl.LocalPlayer == Trapper || PlayerControl.LocalPlayer == Player)
        {
            indicator = new GameObject("StunnedIndicator");
            indicator.transform.SetParent(Player.gameObject.transform);
            indicator.transform.localPosition = new Vector3(0f, 0.8f, 0f);
            Coroutines.Start(DoStunnedAnim(indicator.AddComponent<SpriteRenderer>()));

            arrow = Object.Instantiate(RoleManager.Instance.GetRole(AmongUs.GameOptions.RoleTypes.Noisemaker).Cast<NoisemakerRole>().deathArrowPrefab).GetComponent<NoisemakerArrow>();
            //arrow.target = Player.GetTruePosition();
            //arrow.image.sprite = Assets.Trap0.LoadAsset();
        }
    }
    GameObject indicator;
    public LoadableResourceAsset[] StunnedSprites = new[]
    {
        Assets.Stunned0,
        Assets.Stunned1,
        Assets.Stunned2,
        Assets.Stunned3
    };
    public System.Collections.IEnumerator DoStunnedAnim(SpriteRenderer rend)
    {
        float frameinterval = 0.125f;

        for (int i = 0; i == 3; i++)
        {
            rend.sprite = StunnedSprites[i].LoadAsset();
            yield return new WaitForSeconds(frameinterval);
        }


        Coroutines.Start(DoStunnedAnim(rend));
        yield break;
    }
    public override void OnTimerComplete()
    {
        Player.MyPhysics.enabled = true;
        indicator.gameObject.DestroyImmediate();
    }
}
