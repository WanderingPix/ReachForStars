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
    ManipulatedKill ManipulatedKillButton = CustomButtonSingleton<ManipulatedKill>.Instance;
    public override void OnActivate()
    {
        if (Player == PlayerControl.LocalPlayer)
        {
            HudManager.Instance.StartCoroutine(HudManager.Instance.ShowEmblem(true));

            ManipulatedKillButton.Button.Show();
            label = Object.Instantiate<TextMeshPro>(HudManager.Instance.KillButton.buttonLabelText, HudManager.Instance.transform);
            AspectPosition pos = label.gameObject.AddComponent<AspectPosition>();
            pos.Alignment = AspectPosition.EdgeAlignments.Center;
            pos.DistanceFromEdge = new Vector3(0f, -1f, 0f);
            pos.AdjustPosition();
            label.font = HudManager.Instance.KillButton.buttonLabelText.font;
            label.SetFaceColor(Color.black);
            label.SetOutlineThickness(HudManager.Instance.KillButton.buttonLabelText.outlineWidth);
            label.SetOutlineColor(HudManager.Instance.KillButton.buttonLabelText.outlineColor);
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
    public override void OnDeactivate()
    {
        CustomButtonSingleton<ManipulatedKill>.Instance.Button.Hide();
        Player.RpcCustomMurder(Player, true, true, true, false, false, true);
    }
    public override void OnDeath(DeathReason reason)
    {
        if (PlayerControl.LocalPlayer.Data.Role is ManipulatorRole manip && CustomButtonSingleton<Manipulate>.Instance.CurrentlyManipulatedPlayer == Player)
        {
            Manipulate manipulatebtn = CustomButtonSingleton<Manipulate>.Instance;
            PlayerControl.LocalPlayer.RpcRemoveModifier<ManipulatedModifier>();
            manipulatebtn.overlay.ShowKillAnimation(PlayerControl.LocalPlayer.Data, manipulatebtn.CurrentlyManipulatedPlayer.Data);
        }
    }
    public override bool HideOnUi => true;
}
