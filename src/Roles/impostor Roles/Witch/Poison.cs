using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using MiraAPI.Modifiers;
using MiraAPI.GameOptions;

namespace ReachForStars.Roles.Impostors.Witch;
public class Poison : CustomActionButton<PlayerControl>
{
    public override string Name => "Poison";

    public override float Cooldown => 5;
    public override float EffectDuration => OptionGroupSingleton<WitchOptions>.Instance.PoisonDelay.Value;

    public override ButtonLocation Location => ButtonLocation.BottomLeft;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.PoisonButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is WitchRole;
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance, false);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(Palette.ImpostorRed));
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return true;
    }
    protected override void OnClick()
    {
        Target.RpcAddModifier<PoisonedModifier>();
    }

    public override void OnEffectEnd()
    {
        SoundManager.Instance.PlaySound(PlayerControl.LocalPlayer.KillSfx, false, 0.5f);
    }
}
