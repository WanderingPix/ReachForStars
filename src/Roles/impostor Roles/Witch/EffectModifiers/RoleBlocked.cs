using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using UnityEngine;
using HarmonyLib;
using ReachForStars.Networking;
using ReachForStars.Utilities;
using System.Linq;

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
        foreach (ActionButton button in Object.FindObjectsOfType<ActionButton>())
        {
            
            HudManager.Instance.StartCoroutine(Effects.ScaleIn(button.transform, 0.7f, 0f, 1.5f));
        
            HudManager.Instance.StartCoroutine(Effects.ColorFade(button.graphic, Palette.White, Palette.Black, 1f));
        }
        PlayerControl.LocalPlayer.cosmetics.SetOutline(true, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
    }
    public override void OnDeactivate()
    {
        foreach (ActionButton button in Object.FindObjectsOfType<ActionButton>())
        {
            
            HudManager.Instance.StartCoroutine(Effects.ScaleIn(button.transform, 0f, 0.7f, 1.5f));
        
            HudManager.Instance.StartCoroutine(Effects.ColorFade(button.graphic, Palette.Black, Palette.White, 1f));
        }
        PlayerControl.LocalPlayer.cosmetics.SetOutline(false, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
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
