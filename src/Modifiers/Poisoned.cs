using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using UnityEngine;
using ReachForStars.Networking;
using ReachForStars.Utilities;
using System.Linq;
using MiraAPI.Networking;
using ReachForStars.Roles.Impostors.Witch;

namespace ReachForStars.Addons.Poisoned;

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
        PlayerControl.LocalPlayer.cosmetics.SetOutline(true, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
    }
    public override void OnTimerComplete()
    {
        PlayerControl.LocalPlayer.RpcCustomMurder(PlayerControl.LocalPlayer, true);
        PlayerControl.LocalPlayer.cosmetics.SetOutline(false, new Il2CppSystem.Nullable<Color>(new Color(1f, 0f, 1f, 1f)));
    }
}
