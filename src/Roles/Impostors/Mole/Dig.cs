using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Mole;

public class Dig : CustomActionButton
{
    public TranslationPool buttonName = new(
        "Dig",
        "excavar",
        "creuser",
        "копать"
    );

    public override string Name => buttonName.GetTranslatedText();

    public override float Cooldown => OptionGroupSingleton<MoleOptions>.Instance.DigCD.Value;
    public override float EffectDuration => OptionGroupSingleton<MoleOptions>.Instance.TunnelingDuration.Value;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.DigButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is MoleRole;
    }

    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcAddModifier<TunnelingModifier>();
    }
}