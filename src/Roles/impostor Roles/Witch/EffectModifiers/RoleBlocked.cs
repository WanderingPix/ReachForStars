using System.Collections;
using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using ReachForStars.Features;
using Reactor.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace ReachForStars.Roles.Impostors.Witch;

public class RoleBlockedModifier : GameModifier
{
    public override string ModifierName => "RoleBlocked";

    public override void OnMeetingStart()
    {
        Player.RpcRemoveModifier<RoleBlockedModifier>();
    }

    public override void OnActivate()
    {
        if (Player == PlayerControl.LocalPlayer)
        {
            Coroutines.Start(DoAnimation(true));
            PlayerControl.LocalPlayer.cosmetics.SetOutline(true, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
        }
    }
    public IEnumerator DoAnimation(bool Show)
    {
        foreach (ActionButton button in Object.FindObjectsOfType<ActionButton>(true))
        {
            if (Show)
            {
                HudManager.Instance.StartCoroutine(Effects.ScaleIn(button.transform, 0.7f * SmolUI.ScaleFactor, 0f, 1.5f));

                yield return HudManager.Instance.StartCoroutine(Effects.ColorFade(button.graphic, Palette.White, Palette.Black, 1f));
                button.Hide();
            }
            if (!Show)
            {
                button.Show();
                HudManager.Instance.StartCoroutine(Effects.ScaleIn(button.transform, 0f, 0.7f * SmolUI.ScaleFactor, 1.5f));

                HudManager.Instance.StartCoroutine(Effects.ColorFade(button.graphic, Palette.Black, Palette.White, 1f));
            }
        }
        yield break;
    }
    public override void OnDeactivate()
    {
        if (Player == PlayerControl.LocalPlayer)
        {
            Coroutines.Start(DoAnimation(false));
            PlayerControl.LocalPlayer.cosmetics.SetOutline(false, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
        }
    }

    public override int GetAssignmentChance()
    {
        return 0;
    }

    public override int GetAmountPerGame()
    {
        return 0;
    }
}
