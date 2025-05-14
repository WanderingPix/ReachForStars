using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using UnityEngine;
using MiraAPI.Networking;
using MiraAPI.GameOptions;
using MiraAPI.Roles;
using TMPro;
using Il2CppSystem.Web.Util;
using Reactor.Utilities;
using System.Collections;
using Hazel;
using Reactor.Utilities.Extensions;
using MiraAPI.Hud;

namespace ReachForStars.Roles.Impostors.Manipulator;

public class ManipulatedModifier : TimedModifier
{
    public override string ModifierName => "Manipulated";
    public override float Duration => 25f; //TODO make this a config option

    public override void OnMeetingStart()
    {
        Player.RpcCustomMurder(Player, true, true, true, false, false, true);
    }
    TextMeshPro label;
    KillButton ManipulatedKillButton;
    public override void OnActivate()
    {
        if (Player == PlayerControl.LocalPlayer)
        {
            HudManager.Instance.shhhEmblem.PlayAnimation();

            label = MiraAPI.Utilities.Helpers.CreateTextLabel("Kill someone or die! vewy scawy I know", HudManager.Instance.transform, pos.Alignment, pos.DistanceFromEdge, 3f);
            Coroutines.Start(DoCountdown(label, Duration));
        }
    }
    public IEnumerator DoCountdown(TextMeshPro tmp, float startingcd)
    {
        float currentCD = startingcd;
        HudManager.Instance.StartCoroutine(Effects.ShakeForever(tmp.transform, 0.02f));
        while (currentCD > 0)
        {
            tmp.text = $"Kill someone or die! vewy scawy I know \n\n<size=7>{currentCD}";
            yield return new WaitForSeconds(1f);
            currentCD--;
        }
        tmp.DestroyImmediate();
        yield break;
    }
    public override void OnTimerComplete()
    {
        HudManager.Instance.KillButton.Hide();
        Player.RpcCustomMurder(Player, true, true, true, false, false, true);
    }
}
