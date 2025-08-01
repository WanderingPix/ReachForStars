using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.GhostBuster;

public class VacuumedModifier(PlayerControl GhostBuster) : GameModifier
{
    public PlayerControl GB = GhostBuster; //Ghost Buster

    private TextMeshPro tmp;
    public override string ModifierName { get; } = "Vacuumed";

    public override bool ShowInFreeplay { get; } = false;
    public override bool HideOnUi { get; } = true;

    public override int GetAssignmentChance()
    {
        return 0;
    }

    public override int GetAmountPerGame()
    {
        return 0;
    }

    public override void OnActivate()
    {
        Player.MyPhysics.GhostSpeed /= 0.5f;
        if (Player != PlayerControl.LocalPlayer) return;
        HudManager.Instance.PlayerCam.Target = GB;
        Player.gameObject.SetActive(false);
        HudManager.Instance.SetHudActive(false);

        tmp = Helpers.CreateTextLabel("VacuumedText", HudManager.Instance.transform,
            AspectPosition.EdgeAlignments.Bottom,
            new Vector3(0, 1, 0));

        tmp.text = "You've been vacuumed!\nYou're trapped until the next meeting!";
        tmp.color = RFSPalette.GhostBusterColor;
    }

    public override void OnMeetingStart()
    {
        tmp.gameObject.DestroyImmediate();
        HudManager.Instance.SetHudActive(true);
        HudManager.Instance.PlayerCam.Target = Player;
        Player.moveable = true;
        Player.RemoveModifier<VacuumedModifier>();
    }
}