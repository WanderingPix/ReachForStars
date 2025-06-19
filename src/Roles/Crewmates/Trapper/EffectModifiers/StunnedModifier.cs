using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using UnityEngine;
using MiraAPI.Networking;
using MiraAPI.GameOptions;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Crewmates.Trapper;

public class StunnedModifier : TimedModifier
{
    public override string ModifierName => name.GetTranslatedText();
    public TranslationPool name = new
    (
        english: "Stunned",
        spanish: "Atónito",
        french: "Choc",
        russian: "ошеломленный"
        //italian: ""
    );

    public override float Duration => 3f;

    public PlayerControl Trapper;

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
    GameObject indicator;
    public override void OnTimerComplete()
    {
        Player.MyPhysics.enabled = true;
        indicator.gameObject.DestroyImmediate();
    }
}
