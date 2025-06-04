using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using UnityEngine;
using MiraAPI.Networking;
using MiraAPI.GameOptions;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;

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

            NoisemakerRole noise = RoleManager.Instance.GetRole(AmongUs.GameOptions.RoleTypes.Noisemaker) as NoisemakerRole;
            arrow = Object.Instantiate(noise.deathArrowPrefab).GetComponent<NoisemakerArrow>();
            arrow.pivot = Player.transform;

            arrow.image.sprite = Assets.Trap0.LoadAsset();
        }
    }
    GameObject indicator;
    public System.Collections.IEnumerator DoStunnedAnim(SpriteRenderer rend)
    {
        float frameinterval = 0.125f;

        rend.sprite = Assets.Stunned0.LoadAsset();
        yield return new WaitForSeconds(frameinterval);

        rend.sprite = Assets.Stunned1.LoadAsset();
        yield return new WaitForSeconds(frameinterval); 

        rend.sprite = Assets.Stunned2.LoadAsset();
        yield return new WaitForSeconds(frameinterval);

        rend.sprite = Assets.Stunned3.LoadAsset();
        yield return new WaitForSeconds(frameinterval);


        Coroutines.Start(DoStunnedAnim(rend)); 
        yield break;
    }
    public override void OnTimerComplete()
    {
        Player.MyPhysics.enabled = true;
        indicator.DestroyImmediate();
    }
}
