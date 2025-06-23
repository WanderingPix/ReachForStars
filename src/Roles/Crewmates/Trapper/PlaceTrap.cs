using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Trapper;

public class TrapAbility : CustomActionButton
{
    public TranslationPool ButtonName = new(
        "Trap",
        "Trampa",
        "piéger",
        "Трапнуть"
        //italian: ""
    );

    public override string Name => ButtonName.GetTranslatedText();
    public override Color TextOutlineColor => Palette.CrewmateRoleHeaderBlue;
    public override float Cooldown => 25;
    public override float EffectDuration => 0;

    public override int MaxUses => 3;

    public override LoadableAsset<Sprite> Sprite => Assets.PlaceTrapButton;
    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is TrapperRole;
    }

    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcPlaceTrap();
        SoundManager.Instance.PlaySound(Assets.TrapPlaceSfx.LoadAsset(), false);
    }
}