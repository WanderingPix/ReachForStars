using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using UnityEngine;
using HarmonyLib;
using ReachForStars.Addons;
using TheSillyRoles.RPCHandler;
using ReachForStars.Utilities;
using System.Linq;

namespace ReachForStars.Addons.RoleBlocked;

public class RoleBlockedModifier : TimedModifier
{
    public override string ModifierName => "RoleBlocked";


    public override float Duration => 10; //Add setting here

    public override bool AutoStart => true;

    public override bool RemoveOnComplete => true;

    public override void OnTimerComplete()
    {
        foreach (AbilityButton button in Object.FindObjectsOfType<AbilityButton>())
        {
            
            HudManager.Instance.StartCoroutine(Effects.ScaleIn(button.transform, 0f, 0.7f, 1.5f));
        
            HudManager.Instance.StartCoroutine(Effects.ColorFade(button.graphic, Palette.Black, Palette.White, 1f));
        }
        PlayerControl.LocalPlayer.cosmetics.SetOutline(false, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
    }
    public override void OnActivate()
    {
        foreach (AbilityButton button in Object.FindObjectsOfType<AbilityButton>())
        {
            
            HudManager.Instance.StartCoroutine(Effects.ScaleIn(button.transform, 0.7f, 0f, 1f));
        
            HudManager.Instance.StartCoroutine(Effects.ColorFade(button.graphic, Palette.White, Palette.Black, 1f));
        }
        PlayerControl.LocalPlayer.cosmetics.SetOutline(true, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
    }

    public override bool HideOnUi => true;

    
}