using System.Linq;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Components.Tasks;
using ReachForStars.Networking;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Silencer;

public class SilenceButton : CustomActionButton
{
    public override string Name => "Silence";

    public override float Cooldown => OptionGroupSingleton<SilencerOptions>.Instance.AbilityCooldown.Value;

    public override float EffectDuration => OptionGroupSingleton<SilencerOptions>.Instance.AbilityDuration.Value;

    public override LoadableAsset<Sprite> Sprite => Assets.PlaceHolder;

    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcSilence();
    }

    public override bool Enabled(RoleBehaviour role)
    {
        return role is SilencerRole;
    }

    public override bool CanUse()
    {
        return !PlayerTask.PlayerHasTaskOfType<SilenceTask>(PlayerControl.LocalPlayer);
    }
}