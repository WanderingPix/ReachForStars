using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Bomber;

public class BombAbility : CustomActionButton
{
    public TranslationPool buttonName = new(
        "Bomb",
        "",
        "",
        ""
    );

    public bool HasUsed = true;

    public override string Name => buttonName.GetTranslatedText();

    public override float Cooldown => 0;

    public override float EffectDuration => 0;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.DigButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is BomberRole;
    }

    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcLaunchBomb();
    }

    public override void OnEffectEnd()
    {
    }
}