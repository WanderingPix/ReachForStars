using Il2CppSystem;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Witch;

public class Poison : CustomActionButton<PlayerControl>
{
    public TranslationPool btnName = new(
        "Poison",
        french: "Empoisoner",
        spanish: "Veneno",
        russian: "Яд"
        //italian: "Veleno"
    );

    public override string Name => btnName.GetTranslatedText();

    public override float Cooldown => 5;
    public override float EffectDuration => OptionGroupSingleton<WitchOptions>.Instance.PoisonDelay;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.PoisonButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is WitchRole;
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Nullable<Color>(Palette.ImpostorRed));
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
        SoundManager.Instance.PlaySound(PlayerControl.LocalPlayer.KillSfx, false, 0.5f, SoundManager.Instance.sfxMixer);
    }
}