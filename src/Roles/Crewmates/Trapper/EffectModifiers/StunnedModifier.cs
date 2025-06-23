using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using ReachForStars.Translation;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Trapper;

public class StunnedModifier : TimedModifier
{
    private GameObject indicator;

    public TranslationPool name = new
    (
        "Stunned",
        "Atónito",
        "Choc",
        "ошеломленный"
        //italian: ""
    );

    public PlayerControl Trapper;
    public override string ModifierName => name.GetTranslatedText();

    public override float Duration => 3f;

    public override void OnMeetingStart()
    {
        Player.MyPhysics.enabled = true;
        Player.RemoveModifier<StunnedModifier>();
    }

    public override void OnActivate()
    {
        if (PlayerControl.LocalPlayer == Trapper || PlayerControl.LocalPlayer == Player)
        {
            indicator = Object.Instantiate(Assets.StunnedPrefab.LoadAsset());
            indicator.transform.SetParent(Player.gameObject.transform);
            indicator.transform.localPosition = new Vector3(0f, 0.8f, 0f);
        }

        Player.NetTransform.Halt();
        Player.MyPhysics.enabled = false;
    }

    public override void OnTimerComplete()
    {
        Player.MyPhysics.enabled = true;
        indicator.gameObject.DestroyImmediate();
    }
}