using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using UnityEngine;
using ReachForStars.Networking;
using ReachForStars.Utilities;
using System.Linq;
using MiraAPI.Networking;
using MiraAPI.GameOptions;

namespace ReachForStars.Roles.Impostors.Witch;

public class PoisonedModifier : TimedModifier
{
    public override string ModifierName => "Poisoned";

    public override float Duration => OptionGroupSingleton<WitchOptions>.Instance.PoisonDelay.Value;

    public override void OnMeetingStart()
    {
        Player.RpcCustomMurder(PlayerControl.LocalPlayer, true);
        Player.RpcRemoveModifier<PoisonedModifier>();
    }

    public override void OnActivate()
    {
        Player.cosmetics.SetOutline(true, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
    }
    public override void OnTimerComplete()
    {
        Player.RpcCustomMurder(PlayerControl.LocalPlayer, true);
        Player.cosmetics.SetOutline(false, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
    }
}
