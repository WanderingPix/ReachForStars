using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using UnityEngine;
using TheSillyRoles.RPCHandler;
using ReachForStars.Utilities;
using System.Linq;
using MiraAPI.Networking;
using ReachForStars.Roles.Impostors.Witch;

namespace ReachForStars.Addons.RoleBlocked;

public class PoisonedModifier : TimedModifier
{
    public override string ModifierName => "Poisoned";

    public override float Duration => 25f;

    public override void OnMeetingStart()
    {
        PlayerControl.LocalPlayer.RpcCustomMurder(PlayerControl.LocalPlayer, true);
        Player.RpcRemoveModifier<PoisonedModifier>();
    }

    public override void OnActivate()
    {
        foreach (ActionButton button in Object.FindObjectsOfType<ActionButton>())
        {
            
            HudManager.Instance.StartCoroutine(Effects.ScaleIn(button.transform, 0f, 0.7f, 1.5f));
        
            HudManager.Instance.StartCoroutine(Effects.ColorFade(button.graphic, Palette.Black, Palette.White, 1f));
        }
        PlayerControl.LocalPlayer.cosmetics.SetOutline(true, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
    }
    public override void OnTimerComplete()
    {
        PlayerControl.LocalPlayer.RpcCustomMurder(PlayerControl.LocalPlayer, true);
        PlayerControl.LocalPlayer.cosmetics.SetOutline(false, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
    }

    public override bool HideOnUi => true;
}