using Il2CppSystem;
using MiraAPI.GameOptions;
using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Networking;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Witch;

public class PoisonedModifier : TimedModifier
{
    public override string ModifierName => "Poisoned";

    public override float Duration => OptionGroupSingleton<WitchOptions>.Instance.PoisonDelay;

    public override void OnMeetingStart()
    {
        Player.RpcCustomMurder(PlayerControl.LocalPlayer, true);
        Player.RpcRemoveModifier<PoisonedModifier>();
    }

    public override void OnActivate()
    {
        Player.cosmetics.SetOutline(true, new Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
    }

    public override void OnDeactivate()
    {
        Player.CustomMurder(Player, MurderResultFlags.Succeeded, showKillAnim: false);
        Player.cosmetics.SetOutline(false, new Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
    }
}