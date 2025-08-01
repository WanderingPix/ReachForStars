using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities.Assets;
using ReachForStars.Roles.Neutrals.Roles.GhostBuster;
using ReachForStars.Translation;
using ReachForStars.Utilities;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.GhostBuster;

public class Vacuum : CustomActionButton<PlayerControl>
{
    private static readonly TranslationPool BtnName = new
    (
        "Vacuum"
    );

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override string Name => BtnName.GetTranslatedText();
    public override float Cooldown => 15f;
    public override float EffectDuration => 3f;
    public override LoadableAsset<Sprite> Sprite => Assets.PlaceHolder;

    public override PlayerControl GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestGhost(Distance, true);
    }

    protected override void OnClick()
    {
        Target.RpcAddModifier<ToBeSuckedModifier>(PlayerControl.LocalPlayer);
        SoundManager.Instance.PlaySound(Assets.VacuumGhostSFX.LoadAsset(), false, 1f, SoundManager.instance.sfxMixer);
    }

    public override void OnEffectEnd()
    {
    }

    public override bool Enabled(RoleBehaviour role)
    {
        return role is GhostBusterRole;
    }

    public override void SetOutline(bool active)
    {
        //TODO: Outline method
    }

    public override bool IsTargetValid(PlayerControl target)
    {
        return base.IsTargetValid(target) && !target.HasModifier<ToBeSuckedModifier>() &&
               !target.HasModifier<VacuumedModifier>();
    }
}