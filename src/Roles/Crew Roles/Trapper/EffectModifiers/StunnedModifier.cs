using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using UnityEngine;
using MiraAPI.Networking;
using MiraAPI.GameOptions;

namespace ReachForStars.Roles.Crewmates.Trapper;

public class StunnedModifier : TimedModifier
{
    public override string ModifierName => "Stunned";

    public override float Duration => 15f;

    public override void OnMeetingStart()
    {
        Player.MyPhysics.enabled = true;
        Player.RemoveModifier<StunnedModifier>();
    }

    public override void OnActivate()
    {
    //TODO: Stunned anim
        Player.MyPhysics.enabled = false;
        Player.cosmetics.SetOutline(true, new Il2CppSystem.Nullable<Color>(new Color(1f, 0.84f, 0f, 1f)));
    }
    public override void OnTimerComplete()
    {
        Player.MyPhysics.enabled = true;
        Player.cosmetics.SetOutline(false, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
    }
}
